using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public static class SynStaffMemberMapper
    {
        private const string ENTITY_LOGICAL_NAME = "pgrowth_corestaffmember";
        public static Entity ToCreateEntity(this StaffMemberSnapshot snap, ITracingService tracer)
        {
            var entity = snap.ToEntity(true);

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
        public static Entity ToUpdateEntity(this StaffMemberSnapshot snap, ITracingService tracer)
        {
            if (snap.SynMirrorId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = snap.ToEntity(false);
            entity.Id = snap.SynMirrorId;

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

        public static Entity ToEntity(this StaffMemberSnapshot snap, bool syncAll)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);

            // Identity
            entity[SynStaffMemberFields.SynMirrorId] = snap.SynMirrorId;

            if (!string.IsNullOrWhiteSpace(snap.SourceOfTruthId))
            {
                entity[SynStaffMemberFields.SourceOfTruthId] = snap.SourceOfTruthId;
            }

            entity[SynStaffMemberFields.StaffId] = snap.StaffId;
            entity[SynStaffMemberFields.SchoolStaffCode] = snap.SchoolStaffCode;
            entity[SynStaffMemberFields.CommunityGuid] = snap.CommunityGuid;
            entity[SynStaffMemberFields.ActiveFlag] = snap.ActiveFlag;

            if (snap.Status != null)
            {
                entity[SynStaffMemberFields.Status] = snap.Status;
            }

            if (snap.StatusReason != null)
            {
                entity[SynStaffMemberFields.StatusReason] = snap.StatusReason;
            }

            entity[SynStaffMemberFields.Gender] = snap.Gender;
            entity[SynStaffMemberFields.Given1] = snap.Given1;
            entity[SynStaffMemberFields.Given2] = snap.Given2;
            entity[SynStaffMemberFields.NameInternal] = snap.NameInternal;
            entity[SynStaffMemberFields.MaidenName] = snap.MaidenName;
            entity[SynStaffMemberFields.StaffPreferredName] = snap.StaffPreferredName;
            entity[SynStaffMemberFields.SurnamePrevious] = snap.SurnamePrevious;
            entity[SynStaffMemberFields.Surname] = snap.Surname;
            entity[SynStaffMemberFields.Email] = snap.Email;
            entity[SynStaffMemberFields.OccupEmail] = snap.OccupEmail;
            entity[SynStaffMemberFields.OccupPhone] = snap.OccupPhone;
            entity[SynStaffMemberFields.StaffCampus] = snap.StaffCampus;
            entity[SynStaffMemberFields.StaffCategory] = snap.StaffCategory;
            entity[SynStaffMemberFields.StaffDepartment] = snap.StaffDepartment;
            entity[SynStaffMemberFields.NetworkLogin] = snap.NetworkLogin;
            entity[SynStaffMemberFields.SubSchool] = snap.SubSchool;
            entity[SynStaffMemberFields.CampusKey] = snap.CampusKey;
            entity[SynStaffMemberFields.GenderKey] = snap.GenderKey;
            entity[SynStaffMemberFields.StartDate] = snap.StartDate;
            entity[SynStaffMemberFields.SubSchool] = snap.LeavingDate;

            if (!string.IsNullOrWhiteSpace(snap.PlaintextHash))
            {
                entity[SynStaffMemberFields.PlaintextHash] = snap.PlaintextHash;
            }

            entity[SynStaffMemberFields.EncodedHash] = snap.EncodedHash;

            return entity;
        }
    }
}