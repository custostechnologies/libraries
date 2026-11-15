using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Infrastructure.Http;
using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class CoreStudentFactory
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string _correlationId;
        private bool _resolutionFailed = false;
        private CoreStudents? _coreStudentQuery;
        private bool _create = false;
        public CoreStudentFactory(IDataverseContext context, IDataverseLogger logger, string correlationId)
        {
            _context = context;
            _logger = logger;
            _correlationId = correlationId;
        }
        public (CoreStudent student, bool createAsInactive, string? error) Build(StudentDto snap, string[] attributes)
        {
            _logger.WriteLog("Entered CoreStudentFactory.Build");
            _coreStudentQuery = new CoreStudents(_context, _logger);

            if (snap.StudentId == null) throw new InvalidPluginExecutionException("StudentDto has no StudentID key.");
            int _studentId = (int)snap.StudentId;

            Entity core = _coreStudentQuery.GetEntityByStudentId(_studentId);
            _create = core == null;

            CoreStudent coreStudent;
            if (core != null) _logger.WriteLog("core not null and _create = true, entered coreStudent and setting IsCreate to false", core.Id);
            // create new DTO with only dirty attributes
            coreStudent = core != null
                ? new CoreStudent()
                {
                    IsCreate = false,
                    CoreStudentId = core.Id
                }
                : new CoreStudent()
                {
                    IsCreate = true
                };

            /* update dirty attributes */
            // identity
            MapStudentId(snap, coreStudent);
            MapStudentActiveFlag(snap, coreStudent, attributes);
            MapStatus(snap, coreStudent);
            MapStudentName(snap, coreStudent, attributes);
            MapStudentGender(snap, coreStudent, attributes);
            MapStudentCampus(snap, coreStudent, attributes);
            MapStudentHouse(snap, coreStudent, attributes);
            MapAge(snap, coreStudent, attributes);
            MapStudentYearLevel(snap, coreStudent, attributes);

            // sync data
            MapLastSync(coreStudent);
            if (_create && snap.StudentActiveFlag == false)
            {
                _logger.WriteLog("CREATE CoreStudent with a status of INACTIVE. Returning with createAsInactive = TRUE to perform subsequent UPDATE operation to inactive the record afyter CREATE.");
                return (coreStudent, true, null);
            }
            return (coreStudent, false, null);
        }
        public void SnapshotToEntity(StudentDto snap, string[] attributes)
        {

        }





        private void MapStudentCampus(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapStudentCampus");
            if (_create || attributes.Contains(SynStudentFields.StudentCampus))
            {
                _logger.WriteLog("snap.StudentCampusKey: {0}", snap.StudentCampusKey);
                var campusKey = int.TryParse(snap.StudentCampusKey, out int key) ? key : 384810005; // represets Resolution Error option
                _logger.WriteLog("campusKey parsed to: {0}", key);
                core.Campus = new OptionSetValue(campusKey);
            }
        }
        private void MapStudentGender(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapGender");
            if (_create || attributes.Contains(SynStudentFields.StudentGender))
            {
                _logger.WriteLog("snap.StudentGenderKey: {0}", snap.StudentGenderKey);
                var genderKey = int.TryParse(snap.StudentGenderKey, out int key) ? key : 384810005; // represets Resolution Error option
                _logger.WriteLog("genderKey parsed to: {0}", key);
                core.Gender = new OptionSetValue(genderKey);
            }
        }
        private void MapStudentHouse(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapStudentHouse");
            if (_create || attributes.Contains(SynStudentFields.StudentHouse))
            {
                _logger.WriteLog("snap.StudentHouseKey: {0}", snap.StudentHouseKey);
                var houseKey = int.TryParse(snap.StudentHouseKey, out int key) ? key : 384810005; // represets Resolution Error option
                _logger.WriteLog("houseKey parsed to: {0}", key);
                core.House = new OptionSetValue(houseKey);
            }
        }
        private void MapAge(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapAge.");
            if (_create || attributes.Contains(SynStudentFields.Age))
            {
                core.Age = snap.Age;
                _logger.WriteLog("Set core.Age to: {0}", core.Age);
            }
        }
        private void MapStudentYearLevel(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapStudentYearLevel.");
            if (_create || attributes.Contains(SynStudentFields.StudentYearLevel))
            {
                core.YearLevel = snap.StudentYearLevel;
                _logger.WriteLog("Set core.YearLevel to: {0}", core.YearLevel);
            }
        }
        private void MapStudentName(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapName");
            bool contains = attributes.Contains(SynStudentFields.StudentGiven1)
                || attributes.Contains(SynStudentFields.StudentGiven2)
                || attributes.Contains(SynStudentFields.StudentSurname)
                || attributes.Contains(SynStudentFields.StudentPreferred);

            if (_create || contains)
            {
                core.Given1 = snap.StudentGiven1;
                core.Given2 = snap.StudentGiven2;
                core.Surname = snap.StudentSurname;
                core.Preferred = snap.StudentPreferred;
                core.DisplayName = $"{snap.StudentPreferred} {snap.StudentSurname}";
                _logger.WriteLog("MapName set GivenName1: {0}, GivenName2: {1}, LastName: {2}, PreferredName: {3}",
                    core.Given1,
                    core.Given2,
                    core.Surname,
                    core.Preferred
                );
            }

        }
        private void MapStatus(StudentDto snap, CoreStudent core)
        {
            _logger.WriteLog("In MapStatus with snap.Status: {0}, snap.StatusReason: {1}", snap.Status, snap.StatusReason);
            core.Status = snap.Status;
            core.StatusReason = snap.StatusReason;
        }
        private void MapStudentActiveFlag(StudentDto snap, CoreStudent core, string[] attributes)
        {
            _logger.WriteLog("In MapIsActive.");
            if (_create || attributes.Contains(SynStudentFields.StudentActiveFlag))
            {
                core.ActiveFlag = snap.StudentActiveFlag ?? false;
                _logger.WriteLog("Set core.IsActive to: {0}", core.ActiveFlag);
            }
        }
        private void MapLastSync(CoreStudent core)
        {
            core.LastSync = DateTime.UtcNow;
        }
        private void MapStudentId(StudentDto snap, CoreStudent core)
        {
            if (snap.StudentId != null) core.StudentId = (int)snap.StudentId;
        }
    }
}