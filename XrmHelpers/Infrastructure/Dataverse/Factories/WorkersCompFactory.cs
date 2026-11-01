using System.Reflection;
using spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using System.Text.Json;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class WorkersCompFactory
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        private readonly IDataverseMapper<SafetyIncidentDto> _incidentMapper;
        private readonly IDataverseMapper<WorkersCompensationClaimDto> _workersCompMapper;
        private readonly CoreStaffMembers _coreStaffMemberQuery;
        private readonly EnvironmentVariableValues _envVarQuery;
        private readonly string _claimTargetTeamNameEnvVarName = "pgrowth_envVar_workersCompensationClaim_targetTeamName";
        private readonly bool _isDev;
        private readonly Teams _teamsQuery;
        public WorkersCompFactory(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            IDataverseMapper<SafetyIncidentDto> incidentMapper,
            IDataverseMapper<WorkersCompensationClaimDto> workersCompMapper,
            CoreStaffMembers coreStaffMemberQuery,
            EnvironmentVariableValues envVarQuery,
            Teams teamsQuery
        )
        {
            _logger = loggerFactory.Create<WorkersCompFactory>();
            _context = context;
            _incidentMapper = incidentMapper;
            _workersCompMapper = workersCompMapper;
            _coreStaffMemberQuery = coreStaffMemberQuery;
            _teamsQuery = teamsQuery;
            _envVarQuery = envVarQuery;

            string orgName = _context.Runtime.ExecutionContext.OrganizationName;
            _isDev = orgName.Contains("dev");
        }
        public (Entity newClaim, EntityReference newOwner) Create(Entity incident)
        {
            _logger.WriteLog($"Creating new WorkersCompensationClaim for SafetyIncident with ID: {incident.Id}");
            _logger.WriteLog("Building incident DTO");
            // add complex fields
            WorkersCompensationClaimDto claimDto;
            var incidentDto = _incidentMapper.MapToDto(incident);

            if (incidentDto == null)
            {
                throw new InvalidPluginExecutionException("Unable to resolve SafetyIncident DTO.");
            }

            var staffId = incidentDto.CommunityId;
            var staffEmail = incidentDto.InjuredPersonEmailAddress;

            if (staffId == null && string.IsNullOrWhiteSpace(staffEmail))
            {
                throw new InvalidPluginExecutionException("StaffID and StaffEmail are null, unable to resolve staff member to create a claim.");
            }

            _logger.WriteLog($"Retrieving staff member metadata for StaffID: {staffId}, StaffEmail: {staffEmail}");
            var staffMemberMetadata = staffId != null
                ? _coreStaffMemberQuery.GetMetadataByStaffId((int)staffId)
                : _coreStaffMemberQuery.GetMetadataByOrgEmail(staffEmail!);

            if (staffMemberMetadata == null || staffMemberMetadata.User == null)
            {
                throw new InvalidPluginExecutionException("Unable to resolve staff member to create a claim.");
            }

            _logger.WriteLog($"Retrieved staff member metadata for StaffID: {staffId}, StaffEmail: {staffEmail}");

            var baseTitle = $"CLAIM {staffMemberMetadata.Full} {incidentDto.AutoGenTitle}";
            var title = _isDev
                ? $"TEST {baseTitle}"
                : baseTitle;

            claimDto = new WorkersCompensationClaimDto
            {
                StaffMemberName = staffMemberMetadata.Full,
                StaffMemberEmail = staffMemberMetadata.Email,
                RelatedUserId = staffMemberMetadata.User.Id,
                RelatedUserName = staffMemberMetadata.User.Name,
                StaffType = PersonType.StaffMember,
                Regarding = new EntityReference(EntityLogicalNames.SafetyIncident, incident.Id),
                RegardingId = incident.Id,
                Subject = title
            };

            _logger.WriteLog($"Mapping ClaimDto to TARGET for StaffMember: {claimDto.StaffMemberName}, RelatedUserId: {claimDto.RelatedUserId}, Regarding SafetyIncident ID (claimDto.Regarding.Id): {claimDto.Regarding.Id}");
            var claimEntity = _workersCompMapper.MapToTarget(claimDto);

            if (claimEntity == null)
            {
                throw new InvalidPluginExecutionException("Unable to map ClaimDto to Entity.");
            }

            var newOwnerTeamName = _envVarQuery.GetValue(_claimTargetTeamNameEnvVarName).Value ?? "Debugging Team";

            var newOwnerId = _teamsQuery.ResolveTeamIdByName(newOwnerTeamName);
            _logger.WriteLog($"newOwnerId: {newOwnerId}");
            var newOwnerRef = new EntityReference("team", newOwnerId);
            claimEntity["ownerid"] = newOwnerRef;

            _logger.WriteLog("Creating new claim entity.");
            _logger.WriteLog("LOGGING new entity before create.");
            _logger.WriteLog(JsonSerializer.Serialize(claimEntity, new JsonSerializerOptions { WriteIndented = true }));

            var newId = _context.Runtime.UserService.Create(claimEntity);
            _logger.WriteLog($"Created new claim with ID: {newId}");

            _logger.WriteLog("Retrieving newly created record to pass to notification pipeline");
            var newClaim = _context.Runtime.UserService.Retrieve(
                EntityLogicalNames.WorkersCompensationClaim,
                newId,
                new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
            );

            return (newClaim, newOwnerRef);
        }
    }
}