using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Domain.Enums;
using Resolvers = spgsWebTools.XrmHelpers.Domain.Resolvers;
using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class SynStudentSnapshotFactory
    {
        private readonly ITracingService _tracer;
        public SynStudentSnapshotFactory(ITracingService tracer)
        {
            _tracer = tracer;
        }
        public StudentDto Build(Entity target, Entity? preImage)
        {
            _tracer.Trace("Entered SynStudentSnapshotFactory.Build() with Entity target ID: {0} and Entity preImage ID: {1}",
                target.Id,
                preImage?.Id
            );

            if (target.Contains(SynStudentFields.MutePlugin) && target.GetAttributeValue<bool>(SynStudentFields.MutePlugin))
            {
                _tracer.Trace("MutePlugin is TRUE, setting preImage to NULL to force update using incoming values.");
                preImage = null;
            }

            var snap = new StudentSnapshot()
            {
                // retrieve string values
                StudentCampus = GetAttributeDelta.Execute<string>(SynStudentFields.StudentCampus, target, preImage),
                StudentGender = GetAttributeDelta.Execute<string>(SynStudentFields.StudentGender, target, preImage),
                StudentGiven1 = GetAttributeDelta.Execute<string>(SynStudentFields.StudentGiven1, target, preImage),
                StudentGiven2 = GetAttributeDelta.Execute<string>(SynStudentFields.StudentGiven2, target, preImage),
                StudentHouse = GetAttributeDelta.Execute<string>(SynStudentFields.StudentHouse, target, preImage),
                StudentPreferred = GetAttributeDelta.Execute<string>(SynStudentFields.StudentPreferred, target, preImage),
                StudentSurname = GetAttributeDelta.Execute<string>(SynStudentFields.StudentSurname, target, preImage),

                // retrieve integer values
                StudentId = GetAttributeDelta.Execute<int>(SynStudentFields.StudentId, target, preImage),
                Age = GetAttributeDelta.Execute<int>(SynStudentFields.Age, target, preImage),
                StudentYearLevel = GetAttributeDelta.Execute<int>(SynStudentFields.StudentYearLevel, target, preImage),

                // retrieve boolean values
                StudentActiveFlag = GetAttributeDelta.Execute<bool>(SynStudentFields.StudentActiveFlag, target, preImage),
                IgnoreSync = GetAttributeDelta.Execute<bool>(SynStudentFields.IgnoreSync, target, preImage),
            };

            if (!string.IsNullOrWhiteSpace(snap.StudentCampus))
            {
                var campus = Resolvers.CampusResolver.ToEnum(snap.StudentCampus!);
                snap.StudentCampusKey = ((int)campus).ToString();
            }

            if (!string.IsNullOrWhiteSpace(snap.StudentGender))
            {
                var gender = Resolvers.GenderResolver.ToEnum(snap.StudentGender!);
                snap.StudentGenderKey = ((int)gender).ToString();
            }

            if (!string.IsNullOrWhiteSpace(snap.StudentHouse))
            {
                _tracer.Trace($"snap.StudentHouse: {snap.StudentHouse}");
                var house = Resolvers.HouseResolver.ToEnum(snap.StudentHouse!);
                snap.StudentHouseKey = ((int)house).ToString();
            }

            if (preImage != null)
            {
                snap.SourceRecordId = preImage.Id;
                snap.PlaintextHash = GetAttributeDelta.Execute<string>(SynStudentFields.PlaintextHash, preImage, null);
                snap.EncodedHash = GetAttributeDelta.Execute<int>(SynStudentFields.EncodedHash, preImage, null);
            }

            // update status based on values
            _tracer.Trace("Calculating snapshot status based on StudentActiveFlag.");
            if (snap.StudentActiveFlag == false)
            {
                snap.Status = new OptionSetValue((int)Status.Inactive);
                snap.StatusReason = new OptionSetValue((int)StatusReason.Inactive);
            }
            else
            {
                snap.Status = new OptionSetValue((int)Status.Active);
                snap.StatusReason = new OptionSetValue((int)StatusReason.Inactive);
            }

            _tracer.Trace("snap.Status: {0}, snap.StatusReason: {1}", snap.Status.Value, snap.StatusReason.Value);

            _tracer.Trace("In SynStudentFactory.Build, syn staff member SNAPSHOT object returned: {0}",
                JsonSerializer.Serialize<StudentSnapshot>(
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