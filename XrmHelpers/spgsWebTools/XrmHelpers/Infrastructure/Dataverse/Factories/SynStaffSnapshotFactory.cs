using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.NewUserSyncEvent.Factories
{
    public class SynStaffMemberFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private SynStaffMembers? _synStaffQuery;
        public SynStaffMemberFactory(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public StaffMemberSnapshot Build(Entity target, Entity preImage)
        {
            Enums.Status status = Enums.Status.Active;
            Enums.StatusReason statusReason = Enums.StatusReason.Active;

            _tracer.Trace("Executing SynStaffMemberQueryService.RetrieveEntity for preImage Id: {0}", preImage.Id.ToString());


            _tracer.Trace("In SynStaffMemberFactory.Build, TARGET is: {0}",
                JsonSerializer.Serialize<Entity>(
                    target,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            _tracer.Trace("In SynStaffMemberFactory.Build, PREIMAGE is: {0}",
                JsonSerializer.Serialize<Entity>(
                    preImage,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            var synStaffMember = new StaffMemberSnapshot()
            {
                // retrieve string values
                StaffCampus = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffCampus, target, preImage),
                StaffCategory = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffCategory, target, preImage),
                SchoolStaffCode = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SchoolStaffCode, target, preImage),
                StaffDepartment = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffDepartment, target, preImage),
                Gender = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Gender, target, preImage),
                Given1 = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Given1, target, preImage),
                Given2 = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Given2, target, preImage),
                NameInternal = GetAttributeDelta.Execute<string>(SynStaffMemberFields.NameInternal, target, preImage),
                Surname = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Surname, target, preImage),
                NetworkLogin = GetAttributeDelta.Execute<string>(SynStaffMemberFields.NetworkLogin, target, preImage),
                MaidenName = GetAttributeDelta.Execute<string>(SynStaffMemberFields.MaidenName, target, preImage),
                StaffPreferredName = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffPreferredName, target, preImage),
                Email = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Email, target, preImage),
                SurnamePrevious = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SurnamePrevious, target, preImage),
                SubSchool = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SubSchool, target, preImage),
                CommunityGuid = GetAttributeDelta.Execute<string>(SynStaffMemberFields.CommunityGuid, target, preImage),
                OccupEmail = GetAttributeDelta.Execute<string>(SynStaffMemberFields.OccupEmail, target, preImage),
                OccupPhone = GetAttributeDelta.Execute<string>(SynStaffMemberFields.OccupPhone, target, preImage),

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynStaffMemberFields.StaffId, target, preImage),
                CampusKey = GetAttributeDelta.Execute<string>(SynStaffMemberFields.CampusKey, target, preImage),
                GenderKey = GetAttributeDelta.Execute<string>(SynStaffMemberFields.GenderKey, target, preImage),

                // retrieve boolean values
                ActiveFlag = GetAttributeDelta.Execute<bool>(SynStaffMemberFields.ActiveFlag, target, preImage),

                // retrieve date times values accounting for null values
                LeavingDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.LeavingDate, target, preImage),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.StartDate, target, preImage),

                // retrieve source of truth id
                SourceOfTruthId = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SourceOfTruthId, target, preImage),

                SynMirrorId = preImage.Id,
                // status
                Status = GetAttributeDelta.Execute<OptionSetValue>(SynStaffMemberFields.Status, target, preImage),
                StatusReason = GetAttributeDelta.Execute<OptionSetValue>(SynStaffMemberFields.StatusReason, target, preImage)
            };

            // update status based on values
            if (
                (synStaffMember.LeavingDate.HasValue && synStaffMember.LeavingDate.Value <= DateTime.UtcNow)
                || (!synStaffMember.ActiveFlag)
            )
            {
                synStaffMember.Status = new OptionSetValue((int)Enums.Status.Inactive);
                synStaffMember.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }

            _tracer.Trace("In SynStaffMemberFactory.Build, syn staff member SNAPSHOT object returned: {0}",
                JsonSerializer.Serialize<StaffMemberSnapshot>(
                    synStaffMember,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            return synStaffMember;
        }
        public StaffMemberSnapshot Build(Entity synEntity)
        {

            _tracer.Trace("Executing SynStaffMemberQueryService.RetrieveEntity from single Entity synStaffMember: {0}", synEntity.GetAttributeValue<Guid>("pgrowth_synergetic_vstaff_community_leftjoinid"));


            _tracer.Trace("In SynStaffMemberFactory.Build, synEntity is: {0}",
                JsonSerializer.Serialize<Entity>(
                    synEntity,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            var synStaffMember = new StaffMemberSnapshot()
            {
                // retrieve string values
                StaffCampus = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffCampus, synEntity),
                StaffCategory = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffCategory, synEntity),
                SchoolStaffCode = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SchoolStaffCode, synEntity),
                StaffDepartment = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffDepartment, synEntity),
                Gender = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Gender, synEntity),
                Given1 = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Given1, synEntity),
                Given2 = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Given2, synEntity),
                NameInternal = GetAttributeDelta.Execute<string>(SynStaffMemberFields.NameInternal, synEntity),
                Surname = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Surname, synEntity),
                NetworkLogin = GetAttributeDelta.Execute<string>(SynStaffMemberFields.NetworkLogin, synEntity),
                MaidenName = GetAttributeDelta.Execute<string>(SynStaffMemberFields.MaidenName, synEntity),
                StaffPreferredName = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffPreferredName, synEntity),
                Email = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Email, synEntity),
                SurnamePrevious = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SurnamePrevious, synEntity),
                SubSchool = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SubSchool, synEntity),
                CommunityGuid = GetAttributeDelta.Execute<string>(SynStaffMemberFields.CommunityGuid, synEntity),
                OccupEmail = GetAttributeDelta.Execute<string>(SynStaffMemberFields.OccupEmail, synEntity),
                OccupPhone = GetAttributeDelta.Execute<string>(SynStaffMemberFields.OccupPhone, synEntity),

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynStaffMemberFields.StaffId, synEntity),
                CampusKey = GetAttributeDelta.Execute<string>(SynStaffMemberFields.CampusKey, synEntity),
                GenderKey = GetAttributeDelta.Execute<string>(SynStaffMemberFields.GenderKey, synEntity),

                // retrieve boolean values
                ActiveFlag = GetAttributeDelta.Execute<bool>(SynStaffMemberFields.ActiveFlag, synEntity),

                // retrieve date times values accounting for null values
                LeavingDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.LeavingDate, synEntity),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.StartDate, synEntity),

                // retrieve source of truth id
                SourceOfTruthId = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SourceOfTruthId, synEntity),

                SynMirrorId = synEntity.Id,
                // status
                Status = GetAttributeDelta.Execute<OptionSetValue>(SynStaffMemberFields.Status, synEntity),
                StatusReason = GetAttributeDelta.Execute<OptionSetValue>(SynStaffMemberFields.StatusReason, synEntity)
            };

            _tracer.Trace("In SynStaffMemberFactory.Build, syn staff member SNAPSHOT object returned: {0}",
                JsonSerializer.Serialize<StaffMemberSnapshot>(
                    synStaffMember,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            return synStaffMember;
        }
    }
}