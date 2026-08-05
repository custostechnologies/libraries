using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public static class CoreStaffMemberMapper
    {
        private const string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        public static Entity ToCreateEntity(this CoreStaffMember core, ITracingService tracer)
        {
            var entity = core.ToEntity(true);

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
        public static Entity ToUpdateEntity(this CoreStaffMember core, ITracingService tracer)
        {
            if (core.CoreStaffMemberId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = core.ToEntity(false);
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

        public static Entity ToEntity(this CoreStaffMember core, bool create)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);

            // Identity
            if (!string.IsNullOrWhiteSpace(core.AzureObjectId))
                entity[CoreStaffMemberFields.AzureObjectId] = core.AzureObjectId;

            if (!string.IsNullOrWhiteSpace(core.SourceOfTruthId))
                entity[CoreStaffMemberFields.SourceOfTruthId] = core.SourceOfTruthId;

            if (core.StaffId != null)
                entity[CoreStaffMemberFields.StaffId] = core.StaffId;

            if (core.User != null)
                entity[CoreStaffMemberFields.User] = core.User;

            // Status
            if (core.IsActive != null)
                entity[CoreStaffMemberFields.IsActive] = core.IsActive;

            if (core.Status != null && !create)
                entity[CoreStaffMemberFields.Status] = core.Status;

            if (core.StatusReason != null && !create)
                entity[CoreStaffMemberFields.StatusReason] = core.StatusReason;

            // Job metadata
            if (core.Campus != null)
                entity[CoreStaffMemberFields.Campus] = core.Campus;

            if (core.CoreDepartment != null)
                entity[CoreStaffMemberFields.CoreDepartment] = core.CoreDepartment;

            if (!string.IsNullOrWhiteSpace(core.CoreDepartmentIdx))
                entity[CoreStaffMemberFields.CoreDepartmentIdx] = core.CoreDepartmentIdx;

            if (core.CoreStaffCategory != null)
                entity[CoreStaffMemberFields.CoreStaffCategory] = core.CoreStaffCategory;

            if (!string.IsNullOrWhiteSpace(core.CoreStaffCategoryIdx))
                entity[CoreStaffMemberFields.CoreStaffCategoryIdx] = core.CoreStaffCategoryIdx;

            if (core.StartDate.HasValue)
                entity[CoreStaffMemberFields.StartDate] = core.StartDate.Value;

            if (core.EndDate.HasValue)
                entity[CoreStaffMemberFields.EndDate] = core.EndDate.Value;

            if (!string.IsNullOrWhiteSpace(core.NetworkLogin))
                entity[CoreStaffMemberFields.NetworkLogin] = core.NetworkLogin;

            if (!string.IsNullOrWhiteSpace(core.WorkEmail))
                entity[CoreStaffMemberFields.WorkEmail] = core.WorkEmail;

            // Personal details
            if (!string.IsNullOrWhiteSpace(core.DisplayName))
                entity[CoreStaffMemberFields.DisplayName] = core.DisplayName;

            if (!string.IsNullOrWhiteSpace(core.FullName))
                entity[CoreStaffMemberFields.FullName] = core.FullName;

            if (core.Gender != null)
                entity[CoreStaffMemberFields.Gender] = core.Gender;

            if (!string.IsNullOrWhiteSpace(core.PreferredName))
                entity[CoreStaffMemberFields.PreferredName] = core.PreferredName;

            if (!string.IsNullOrWhiteSpace(core.GivenName1))
                entity[CoreStaffMemberFields.GivenName1] = core.GivenName1;

            if (!string.IsNullOrWhiteSpace(core.GivenName2))
                entity[CoreStaffMemberFields.GivenName2] = core.GivenName2;

            if (!string.IsNullOrWhiteSpace(core.LastName))
                entity[CoreStaffMemberFields.LastName] = core.LastName;

            if (!string.IsNullOrWhiteSpace(core.PreviousLastName))
                entity[CoreStaffMemberFields.PreviousLastName] = core.PreviousLastName;

            if (!string.IsNullOrWhiteSpace(core.MaidenName))
                entity[CoreStaffMemberFields.MaidenName] = core.MaidenName;

            if (!string.IsNullOrWhiteSpace(core.PersonalEmail))
                entity[CoreStaffMemberFields.PersonalEmail] = core.PersonalEmail;

            // Group membership
            if (core.InUserGroup != null)
                entity[CoreStaffMemberFields.InUserGroup] = core.InUserGroup;

            // Flags
            if (core.ProfileResolutionFlag != null)
                entity[CoreStaffMemberFields.ProfileResolutionFlag] = core.ProfileResolutionFlag;

            if (core.SPGSEmailAddressFlag != null)
                entity[CoreStaffMemberFields.SPGSEmailAddressFlag] = core.SPGSEmailAddressFlag;

            if (core.StaffEmailAddressFlag != null)
                entity[CoreStaffMemberFields.StaffEmailAddressFlag] = core.StaffEmailAddressFlag;

            // Sync data
            if (core.LastSync.HasValue)
                entity[CoreStaffMemberFields.LastSync] = core.LastSync.Value;

            return entity;
        }
    }
}