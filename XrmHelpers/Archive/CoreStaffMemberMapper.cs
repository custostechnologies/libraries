using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using System.Text.Json;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class CoreStaffMemberMapper
    {
        private const string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        private readonly IDataverseLogger _logger;
        public CoreStaffMemberMapper(IDataverseLogger logger)
        {
            _logger = logger;
        }
        public Entity ToCreateEntity(CoreStaffMember core, string[] updatedAttributes)
        {
            var entity = ToEntity(core, true, updatedAttributes);
            return entity;
        }
        public Entity ToUpdateEntity(CoreStaffMember core, string[] updatedAttributes)
        {
            if (core.CoreStaffMemberId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = ToEntity(core, false, updatedAttributes);
            entity.Id = core.CoreStaffMemberId;
            return entity;
        }

        public Entity ToEntity(CoreStaffMember core, bool create, string[] updatedAttributes)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);

            // Identity
            if (!string.IsNullOrWhiteSpace(core.AzureObjectId))
                entity[CoreStaffMemberFields.AzureObjectId] = core.AzureObjectId;

            if (!string.IsNullOrWhiteSpace(core.SourceOfTruthId) && updatedAttributes.Contains("pgrowth_sourceoftruthid"))
                entity[CoreStaffMemberFields.SourceOfTruthId] = core.SourceOfTruthId;

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.StaffId,
                CoreStaffMemberFields.StaffId,
                core.StaffId);

            if (core.User != null)
                entity[CoreStaffMemberFields.User] = core.User;

            // Status
            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.ActiveFlag,
                CoreStaffMemberFields.IsActive,
                core.IsActive);

            _logger.WriteLog($"create is: {create}");
            if (create == false)
            {
                MapDelta(entity, updatedAttributes,
                    SynStaffMemberFields.Status,
                    CoreStaffMemberFields.Status,
                    core.Status);
                _logger.WriteLog("Mapped core.Status: {0}, Enum: {1}", core.Status.ToString(), ((Enums.Status)core.Status.Value).ToString());

                MapDelta(entity, updatedAttributes,
                    SynStaffMemberFields.StatusReason,
                    CoreStaffMemberFields.StatusReason,
                    core.StatusReason);
                _logger.WriteLog("Mapped core.StatusReason: {0}, Enum: {1}", core.StatusReason.ToString(), ((Enums.StatusReason)core.StatusReason.Value).ToString());
            }

            // Job metadata
            if (core.CoreDepartment != null)
                entity[CoreStaffMemberFields.CoreDepartment] = core.CoreDepartment;

            if (!string.IsNullOrWhiteSpace(core.CoreDepartmentIdx))
                entity[CoreStaffMemberFields.CoreDepartmentIdx] = core.CoreDepartmentIdx;

            if (core.CoreStaffCategory != null)
                entity[CoreStaffMemberFields.CoreStaffCategory] = core.CoreStaffCategory;

            if (!string.IsNullOrWhiteSpace(core.CoreStaffCategoryIdx))
                entity[CoreStaffMemberFields.CoreStaffCategoryIdx] = core.CoreStaffCategoryIdx;

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.StartDate,
                CoreStaffMemberFields.StartDate,
                core.StartDate);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.LeavingDate,
                CoreStaffMemberFields.EndDate,
                core.EndDate);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.NetworkLogin,
                CoreStaffMemberFields.NetworkLogin,
                core.NetworkLogin);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.OccupEmail,
                CoreStaffMemberFields.WorkEmail,
                core.WorkEmail);

            // Personal details
            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.NameInternal,
                CoreStaffMemberFields.DisplayName,
                core.DisplayName);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.NameInternal,
                CoreStaffMemberFields.FullName,
                core.FullName);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.StaffCampus,
                CoreStaffMemberFields.Campus,
                core.Campus);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.Gender,
                CoreStaffMemberFields.Gender,
                core.Gender);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.StaffPreferredName,
                CoreStaffMemberFields.PreferredName,
                core.PreferredName);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.Given1,
                CoreStaffMemberFields.GivenName1,
                core.GivenName1);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.Given2,
                CoreStaffMemberFields.GivenName2,
                core.GivenName2);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.Surname,
                CoreStaffMemberFields.LastName,
                core.LastName);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.SurnamePrevious,
                CoreStaffMemberFields.PreviousLastName,
                core.PreviousLastName);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.MaidenName,
                CoreStaffMemberFields.MaidenName,
                core.MaidenName);

            MapDelta(entity, updatedAttributes,
                SynStaffMemberFields.Email,
                CoreStaffMemberFields.PersonalEmail,
                core.PersonalEmail);

            // Flags
            if (core.InUserGroup != null)
                entity[CoreStaffMemberFields.InUserGroup] = core.InUserGroup;

            if (core.ProfileResolutionFlag != null)
                entity[CoreStaffMemberFields.ProfileResolutionFlag] = core.ProfileResolutionFlag;

            if (core.SPGSEmailAddressFlag != null)
                entity[CoreStaffMemberFields.SPGSEmailAddressFlag] = core.SPGSEmailAddressFlag;

            if (core.StaffEmailAddressFlag != null)
                entity[CoreStaffMemberFields.StaffEmailAddressFlag] = core.StaffEmailAddressFlag;

            entity[CoreStaffMemberFields.LastSync] = DateTime.UtcNow;
            _logger.WriteLog("Serialised before exiting mapper: {0}",
                JsonSerializer.Serialize(
                    core,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
            _logger.WriteLog("Exiting CoreStaffMemberMapper");
            return entity;
        }
        private void MapDelta<T>(
            Entity entity,
            string[] updatedAttributes,
            string sourceAttribute,
            string targetAttribute,
            T value
        )
        {
            _logger.WriteLog("MapDelta src: {0}, target: {1}, value: {2}",
                sourceAttribute,
                targetAttribute,
                value
            );

            if (updatedAttributes.Contains(sourceAttribute))
            {
                entity[targetAttribute] = value;
            }

            _logger.WriteLog("MapDelta complete, returning to caller.");
        }
    }
}