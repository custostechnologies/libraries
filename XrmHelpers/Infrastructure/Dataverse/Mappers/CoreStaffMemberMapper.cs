using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;
<<<<<<< HEAD

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
=======
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using System.Text.Json;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class CoreStaffMemberMapper
    {
        private const string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        private readonly ITracingService _tracer;
        public CoreStaffMemberMapper(ITracingService tracer)
        {
            _tracer = tracer;
        }
        public Entity ToCreateEntity(CoreStaffMember core, string[] updatedAttributes)
        {
            var entity = ToEntity(core, true, updatedAttributes);
            return entity;
        }
        public Entity ToUpdateEntity(CoreStaffMember core, string[] updatedAttributes)
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        {
            if (core.CoreStaffMemberId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

<<<<<<< HEAD
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
=======
            var entity = ToEntity(core, false, updatedAttributes);
            entity.Id = core.CoreStaffMemberId;
            return entity;
        }

        public Entity ToEntity(CoreStaffMember core, bool create, string[] updatedAttributes)
>>>>>>> source-temp/refactor/decouple-xrmhelpers
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

<<<<<<< HEAD
            if (!create)
=======
            _tracer.Trace($"create is: {create}");
            if (create == false)
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            {
                MapDelta(entity, updatedAttributes,
                    SynStaffMemberFields.Status,
                    CoreStaffMemberFields.Status,
                    core.Status);
<<<<<<< HEAD
=======
                _tracer.Trace("Mapped core.Status: {0}, Enum: {1}", core.Status.ToString(), ((Enums.Status)core.Status.Value).ToString());
>>>>>>> source-temp/refactor/decouple-xrmhelpers

                MapDelta(entity, updatedAttributes,
                    SynStaffMemberFields.StatusReason,
                    CoreStaffMemberFields.StatusReason,
                    core.StatusReason);
<<<<<<< HEAD
=======
                _tracer.Trace("Mapped core.StatusReason: {0}, Enum: {1}", core.StatusReason.ToString(), ((Enums.StatusReason)core.StatusReason.Value).ToString());
>>>>>>> source-temp/refactor/decouple-xrmhelpers
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
<<<<<<< HEAD
=======
                SynStaffMemberFields.StaffCampus,
                CoreStaffMemberFields.Campus,
                core.Campus);

            MapDelta(entity, updatedAttributes,
>>>>>>> source-temp/refactor/decouple-xrmhelpers
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
<<<<<<< HEAD

            return entity;
        }
        private static void MapDelta<T>(
=======
            _tracer.Trace("Serialised before exiting mapper: {0}",
                JsonSerializer.Serialize(
                    core,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
            _tracer.Trace("Exiting CoreStaffMemberMapper");
            return entity;
        }
        private void MapDelta<T>(
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            Entity entity,
            string[] updatedAttributes,
            string sourceAttribute,
            string targetAttribute,
            T value
        )
        {
<<<<<<< HEAD
=======
            _tracer.Trace("MapDelta src: {0}, target: {1}, value: {2}",
                sourceAttribute,
                targetAttribute,
                value
            );

>>>>>>> source-temp/refactor/decouple-xrmhelpers
            if (updatedAttributes.Contains(sourceAttribute))
            {
                entity[targetAttribute] = value;
            }
<<<<<<< HEAD
=======

            _tracer.Trace("MapDelta complete, returning to caller.");
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        }
    }
}