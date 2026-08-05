using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;
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
        public StaffMemberSnapshot Build(Guid synMirrorId)
        {
            // retrieve synergetic staff member
            _synStaffQuery = new SynStaffMembers(_service, _tracer);

            _tracer.Trace("Executing SynStaffMemberQueryService.RetrieveEntity for synMirrorId: {0}", synMirrorId.ToString());

            var syn = _synStaffQuery.GetEntityById(synMirrorId, 1, SynStaffMemberSchema.SyncColumns);

            _tracer.Trace("In SynStaffMemberFactory.Build, syn query entity returned: {0}",
                JsonSerializer.Serialize<Entity>(
                    syn,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );

            _tracer.Trace("SynStaffMemberQueryService.RetrieveEntity complete, with Id: {0}", syn.Id.ToString());

            var synStaffMember = new StaffMemberSnapshot()
            {
                // retrieve string values
                StaffCampus = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffCampus, syn),
                StaffCategory = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffCategory, syn),
                SchoolStaffCode = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SchoolStaffCode, syn),
                StaffDepartment = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffDepartment, syn),
                Gender = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Gender, syn),
                Given1 = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Given1, syn),
                Given2 = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Given2, syn),
                NameInternal = GetAttributeDelta.Execute<string>(SynStaffMemberFields.NameInternal, syn),
                Surname = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Surname, syn),
                NetworkLogin = GetAttributeDelta.Execute<string>(SynStaffMemberFields.NetworkLogin, syn),
                MaidenName = GetAttributeDelta.Execute<string>(SynStaffMemberFields.MaidenName, syn),
                StaffPreferredName = GetAttributeDelta.Execute<string>(SynStaffMemberFields.StaffPreferredName, syn),
                Email = GetAttributeDelta.Execute<string>(SynStaffMemberFields.Email, syn),
                SurnamePrevious = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SurnamePrevious, syn),
                SubSchool = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SubSchool, syn),
                CommunityGuid = GetAttributeDelta.Execute<string>(SynStaffMemberFields.CommunityGuid, syn),
                OccupEmail = GetAttributeDelta.Execute<string>(SynStaffMemberFields.OccupEmail, syn),
                OccupPhone = GetAttributeDelta.Execute<string>(SynStaffMemberFields.OccupPhone, syn),

                // retrieve integer values
                StaffId = GetAttributeDelta.Execute<int>(SynStaffMemberFields.StaffId, syn),
                CampusKey = GetAttributeDelta.Execute<string>(SynStaffMemberFields.CampusKey, syn),
                GenderKey = GetAttributeDelta.Execute<string>(SynStaffMemberFields.GenderKey, syn),

                // retrieve boolean values
                ActiveFlag = GetAttributeDelta.Execute<bool>(SynStaffMemberFields.ActiveFlag, syn),

                // retrieve date times values accounting for null values
                LeavingDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.LeavingDate, syn),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.StartDate, syn),

                // retrieve source of truth id
                SourceOfTruthId = GetAttributeDelta.Execute<string>(SynStaffMemberFields.SourceOfTruthId, syn),
                SynMirrorId = synMirrorId,

                // status
                Status = GetAttributeDelta.Execute<OptionSetValue>(SynStaffMemberFields.Status, syn),
                StatusReason = GetAttributeDelta.Execute<OptionSetValue>(SynStaffMemberFields.StatusReason, syn)
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