using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public static class CoreStaffMemberMapper
    {
        private const string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        public static Entity ToCreateEntity(this CoreStaffMember core, ITracingService tracer, string[] updatedAttributes)
        {
            var entity = core.ToEntity(true, updatedAttributes);

            foreach (var a in entity.Attributes)
            {
                tracer.Trace(
                    "{0} = {1}",
                    a.Key,
                    a.Value?.ToString() ?? "<null>"
                );
            }

            return entity;
        }
        public static Entity ToUpdateEntity(this CoreStaffMember core, ITracingService tracer, string[] updatedAttributes)
        {
            if (core.CoreStaffMemberId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = core.ToEntity(false, updatedAttributes);
            entity.Id = core.CoreStaffMemberId;

            foreach (var a in entity.Attributes)
            {
                tracer.Trace(
                    "{0} = {1}",
                    a.Key,
                    a.Value?.ToString() ?? "<null>"
                );
            }

            return entity;
        }

        public static Entity ToEntity(this CoreStaffMember core, bool create, string[] updatedAttributes)
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

            if (!create)
            {
                MapDelta(entity, updatedAttributes,
                    SynStaffMemberFields.Status,
                    CoreStaffMemberFields.Status,
                    core.Status);

                MapDelta(entity, updatedAttributes,
                    SynStaffMemberFields.StatusReason,
                    CoreStaffMemberFields.StatusReason,
                    core.StatusReason);
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

            return entity;
        }
        private static void MapDelta<T>(
            Entity entity,
            string[] updatedAttributes,
            string sourceAttribute,
            string targetAttribute,
            T value
        )
        {
            if (updatedAttributes.Contains(sourceAttribute))
            {
                entity[targetAttribute] = value;
            }
        }
    }
}