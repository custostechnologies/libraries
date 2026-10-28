using System.Reflection;
using spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class WorkersCompFactory
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        private readonly IDataverseMapper<SafetyIncidentDto> _incidentMapper;
        private readonly IDataverseMapper<WorkersCompensationClaimDto> _workersCompMapper;
        private readonly CoreStaffMembers _coreStaffMemberQuery;
        private bool IsDevEnv = false;
        private readonly Teams _teamsQuery;
        public WorkersCompFactory(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            IDataverseMapper<SafetyIncidentDto> incidentMapper,
            IDataverseMapper<WorkersCompensationClaimDto> workersCompMapper,
            CoreStaffMembers coreStaffMemberQuery,
            Teams teamsQuery
        )
        {
            _logger = loggerFactory.Create<WorkersCompFactory>();
            _context = context;
            _incidentMapper = incidentMapper;
            _workersCompMapper = workersCompMapper;
            _coreStaffMemberQuery = coreStaffMemberQuery;
            _teamsQuery = teamsQuery;

            string orgName = _context.Runtime.ExecutionContext.OrganizationName;
            if (orgName.Contains("dev")) IsDevEnv = true;
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
            claimDto = new WorkersCompensationClaimDto
            {
                StaffMemberName = staffMemberMetadata.Full,
                StaffMemberEmail = staffMemberMetadata.Email,
                RelatedUserId = staffMemberMetadata.User.Id,
                RelatedUserName = staffMemberMetadata.User.Name,
                StaffType = PersonType.StaffMember,
                Regarding = new EntityReference(EntityLogicalNames.SafetyIncident, incident.Id),
                Subject = $"CLAIM {staffMemberMetadata.Full} {incidentDto.AutoGenTitle}"
            };

            _logger.WriteLog($"Mapping ClaimDto to Entity for StaffMember: {claimDto.StaffMemberName}, RelatedUserId: {claimDto.RelatedUserId}, Regarding SafetyIncident ID: {incident.Id}");
            var claimEntity = _workersCompMapper.MapToSource(claimDto);

            if (claimEntity == null)
            {
                throw new InvalidPluginExecutionException("Unable to map ClaimDto to Entity.");
            }

            var newOwnerTeamName = IsDevEnv
                ? "Debugging Team"
                : "People and Culture Manager";

            var newOwnerId = _teamsQuery.ResolveTeamIdByName(newOwnerTeamName);
            var newOwnerRef = new EntityReference("team", newOwnerId);
            claimEntity["ownerid"] = newOwnerRef;

            _logger.WriteLog("Creating new claim entity.");
            var newId = _context.Runtime.UserService.Create(claimEntity);
            _logger.WriteLog($"Created new claim with ID: {newId}");

            claimEntity.Id = newId;
            return (claimEntity, newOwnerRef);
        }
    }
}