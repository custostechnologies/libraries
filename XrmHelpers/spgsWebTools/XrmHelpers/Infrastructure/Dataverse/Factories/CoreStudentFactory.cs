using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Http;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class CoreStudentFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly string _correlationId;
        private bool _resolutionFailed = false;
        private CoreStudents? _coreStudentQuery;
        private bool _create = false;
        public CoreStudentFactory(IOrganizationService service, ITracingService tracer, string correlationId)
        {
            _service = service;
            _tracer = tracer;
            _correlationId = correlationId;
        }
        public (CoreStudent student, bool createAsInactive, string? error) Build(StudentSnapshot syn, string[] attributes)
        {
            _tracer.Trace("Entered CoreStudentFactory.Build");
            _coreStudentQuery = new CoreStudents(_service, _tracer);

            _tracer.Trace("No Core Student Member found via GetEntityByTruth. Trying again with StudentId");

            Entity core = _coreStudentQuery.GetEntityByStudentId(syn.StudentId);

            _create = core == null;

            CoreStudent coreStudent;
            if (core != null) _tracer.Trace("core not null and _create = true, entered coreStudent and setting IsCreate to false", core.Id);
            // create new DTO with only dirty attributes
            coreStudent = core != null
                ? new CoreStudent()
                {
                    IsCreate = _create,
                    CoreStudentId = core.Id
                }
                : new CoreStudent()
                {
                    IsCreate = _create
                };

            /* update dirty attributes */
            // identity
            MapStudentId(syn, coreStudent);
            MapStudentActiveFlag(syn, coreStudent, attributes);
            MapStatus(syn, coreStudent);
            MapStudentName(syn, coreStudent, attributes);
            MapStudentGender(syn, coreStudent, attributes);
            MapStudentCampus(syn, coreStudent, attributes);
            MapStudentHouse(syn, coreStudent, attributes);
            MapAge(syn, coreStudent, attributes);
            MapStudentYearLevel(syn, coreStudent, attributes);

            // sync data
            MapLastSync(coreStudent);
            if (_create && syn.StudentActiveFlag == false)
            {
                _tracer.Trace("CREATE CoreStudent with a status of INACTIVE. Returning with createAsInactive = TRUE to perform subsequent UPDATE operation to inactive the record afyter CREATE.");
                return (coreStudent, true, null);
            }
            return (coreStudent, false, null);
        }
        private void MapStudentCampus(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapStudentCampus");
            if (_create || attributes.Contains(SynStudentFields.StudentCampus))
            {
                _tracer.Trace("syn.StudentCampusKey: {0}", syn.StudentCampusKey);
                var campusKey = int.TryParse(syn.StudentCampusKey, out int key) ? key : 384810005; // represets Resolution Error option
                _tracer.Trace("campusKey parsed to: {0}", key);
                target.Campus = new OptionSetValue(campusKey);
            }
        }
        private void MapStudentGender(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapGender");
            if (_create || attributes.Contains(SynStudentFields.StudentGender))
            {
                _tracer.Trace("syn.StudentGenderKey: {0}", syn.StudentGenderKey);
                var genderKey = int.TryParse(syn.StudentGenderKey, out int key) ? key : 384810005; // represets Resolution Error option
                _tracer.Trace("genderKey parsed to: {0}", key);
                target.Gender = new OptionSetValue(genderKey);
            }
        }
        private void MapStudentHouse(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapStudentHouse");
            if (_create || attributes.Contains(SynStudentFields.StudentHouse))
            {
                _tracer.Trace("syn.StudentHouseKey: {0}", syn.StudentHouseKey);
                var houseKey = int.TryParse(syn.StudentHouseKey, out int key) ? key : 384810005; // represets Resolution Error option
                _tracer.Trace("houseKey parsed to: {0}", key);
                target.House = new OptionSetValue(houseKey);
            }
        }
        private void MapAge(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapAge.");
            if (_create || attributes.Contains(SynStudentFields.Age))
            {
                target.Age = syn.Age;
                _tracer.Trace("Set target.Age to: {0}", target.Age);
            }
        }
        private void MapStudentYearLevel(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapStudentYearLevel.");
            if (_create || attributes.Contains(SynStudentFields.StudentYearLevel))
            {
                target.YearLevel = syn.StudentYearLevel;
                _tracer.Trace("Set target.YearLevel to: {0}", target.YearLevel);
            }
        }
        private void MapStudentName(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapName");
            bool contains = attributes.Contains(SynStudentFields.StudentGiven1)
                || attributes.Contains(SynStudentFields.StudentGiven2)
                || attributes.Contains(SynStudentFields.StudentSurname)
                || attributes.Contains(SynStudentFields.StudentPreferred);

            if (_create || contains)
            {
                target.Given1 = syn.StudentGiven1;
                target.Given2 = syn.StudentGiven2;
                target.Surname = syn.StudentSurname;
                target.Preferred = syn.StudentPreferred;
                target.DisplayName = $"{syn.StudentPreferred} {syn.StudentSurname}";
                _tracer.Trace("MapName set GivenName1: {0}, GivenName2: {1}, LastName: {2}, PreferredName: {3}, DisplayName: {4}, FullName: {5}",
                    target.Given1,
                    target.Given2,
                    target.Surname,
                    target.Preferred,
                    target.DisplayName
                );
            }

        }
        private void MapStatus(StudentSnapshot syn, CoreStudent target)
        {
            _tracer.Trace("In MapStatus with syn.Status: {0}, syn.StatusReason: {1}", syn.Status, syn.StatusReason);
            target.Status = syn.Status;
            target.StatusReason = syn.StatusReason;
        }
        private void MapStudentActiveFlag(StudentSnapshot syn, CoreStudent target, string[] attributes)
        {
            _tracer.Trace("In MapIsActive.");
            if (_create || attributes.Contains(SynStudentFields.StudentActiveFlag))
            {
                target.ActiveFlag = syn.StudentActiveFlag ?? false;
                _tracer.Trace("Set target.IsActive to: {0}", target.ActiveFlag);
            }
        }
        private void MapLastSync(CoreStudent target)
        {
            target.LastSync = DateTime.UtcNow;
        }
        private void MapStudentId(StudentSnapshot syn, CoreStudent target)
        {
            target.StudentId = syn.StudentId;
        }
    }
}