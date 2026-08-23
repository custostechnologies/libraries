using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class SynStaffSnaphotFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private bool _forceIncoming = false;
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

            if (target.Contains(SynStaffMemberFields.MutePlugin))
            {
                _forceIncoming = target.GetAttributeValue<bool>(SynStaffMemberFields.MutePlugin);
                _tracer.Trace("MutePlugin is TRUE, setting preImage to NULL to force update using incoming values.");
                preImage = null;
            }

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

            if (string.IsNullOrWhiteSpace(snap.StaffCategory)) snap.StaffCategory = "NOTSEL";
            if (string.IsNullOrWhiteSpace(snap.StaffDepartment)) snap.StaffDepartment = "NOTSPEC";
            if (string.IsNullOrWhiteSpace(snap.StaffCampus)) snap.CampusKey = Enums.Campus.OtherOrNone.ToString();
            if (string.IsNullOrWhiteSpace(snap.Gender)) snap.GenderKey = Enums.Gender.NotSpecified.ToString();

            if (preImage != null)
            {
                snap.SynMirrorId = preImage.Id;
                snap.PlaintextHash = GetAttributeDelta.Execute<string>(SynStaffMemberFields.PlaintextHash, preImage, null);
                snap.EncodedHash = GetAttributeDelta.Execute<int>(SynStaffMemberFields.EncodedHash, preImage, null);
            }
            // update status based on values
            _tracer.Trace("Calculating snapshot status based on ActiveFlag and LeavingDate.");
            if (snap.ActiveFlag == false || (snap.LeavingDate.HasValue && snap.LeavingDate.Value <= DateTime.UtcNow))
            {
                _tracer.Trace("Setting snap.Status and snap.StatusReason to INACTIVE.");
                snap.Status = new OptionSetValue((int)Enums.Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)Enums.StatusReason.Inactive);
            }
            else
            {
                _tracer.Trace("Setting snap.Status and snap.StatusReason to status: {0} and statusReason: {1}", status.ToString(), statusReason.ToString());
                snap.Status = new OptionSetValue((int)status);
                snap.StatusReason = new OptionSetValue((int)statusReason);
            }

            _tracer.Trace("snap.Status: {0}, snap.StatusReason: {1}", snap.Status.Value, snap.StatusReason.Value);

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