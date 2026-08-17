using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class SynStaffSnaphotFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public SynStaffSnaphotFactory(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public StaffMemberSnapshot Build(Entity target, Entity? preImage)
        {
            _tracer.Trace("Entered SynStaffSnapshotFactory.Build() with targetId: {0} and preImageId: {1}",
                target.Id,
                preImage?.Id
            );

            Enums.Status status = Enums.Status.Active;
            Enums.StatusReason statusReason = Enums.StatusReason.Active;

            _tracer.Trace("In SynStaffMemberFactory.Build, TARGET id: {0}", target.Id);

            if (preImage != null)
            {
                _tracer.Trace("Executing SynStaffMemberQueryService.RetrieveEntity for preImage Id: {0}", preImage.Id.ToString());
            }

            var snap = new StaffMemberSnapshot()
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
                IgnoreSync = GetAttributeDelta.Execute<bool>(SynStaffMemberFields.IgnoreSync, target, preImage),

                // retrieve date times values accounting for null values
                LeavingDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.LeavingDate, target, preImage),
                StartDate = GetAttributeDelta.Execute<DateTime?>(SynStaffMemberFields.StartDate, target, preImage),

                // retrieve source of truth id
                SourceOfTruthId = string.IsNullOrWhiteSpace(GetAttributeDelta.Execute<string>(SynStaffMemberFields.SourceOfTruthId, target, preImage))
                    ? Guid.NewGuid().ToString()
                    : GetAttributeDelta.Execute<string>(SynStaffMemberFields.SourceOfTruthId, target, preImage),
            };

            if (preImage != null)
            {
                snap.SynMirrorId = preImage.Id;
                snap.PlaintextHash = GetAttributeDelta.Execute<string>(SynStaffMemberFields.PlaintextHash, preImage, null);
                snap.EncodedHash = GetAttributeDelta.Execute<int>(SynStaffMemberFields.EncodedHash, preImage, null);
            }
            // update status based on values
            if (
                ((snap.LeavingDate.HasValue && snap.LeavingDate.Value <= DateTime.UtcNow)
                || (!snap.ActiveFlag))
                && (snap.Status == new OptionSetValue((int)status))
            )
            {
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                snap.Status = new OptionSetValue((int)status);
                snap.StatusReason = new OptionSetValue((int)statusReason);
            }

            _tracer.Trace("In SynStaffMemberFactory.Build, syn staff member SNAPSHOT object returned: {0}",
                JsonSerializer.Serialize<StaffMemberSnapshot>(
                    snap,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
            return snap;
        }
    }
}