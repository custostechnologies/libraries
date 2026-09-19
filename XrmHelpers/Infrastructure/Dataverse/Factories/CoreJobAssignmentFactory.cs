<<<<<<< HEAD
=======
using System.Runtime.CompilerServices;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
<<<<<<< HEAD
=======
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class CoreJobAssignmentFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private CoreJobAssignments? _coreJobAssignmentQuery;
        private CoreJobPositions? _coreJobPositionQuery;
        private CoreStaffMembers? _coreStaffMemberQuery;
<<<<<<< HEAD
=======
        private bool _create = false;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        public CoreJobAssignmentFactory(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public CoreJobAssignment Build(JobPositionSnapshot snap, string[] attributes)
        {
<<<<<<< HEAD
            Entity coreAssignment;

            _tracer.Trace("Entered CoreJobAssignmentFactory.Build");
            _tracer.Trace("attributes string[]: {0}",
                JsonSerializer.Serialize(
                    attributes,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                )
            );
=======
            Entity existingCoreAssignment = null;

            _tracer.Trace("Entered CoreJobAssignmentFactory.Build");
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            _tracer.Trace("Initialising query obejcts");

            _coreJobAssignmentQuery = new CoreJobAssignments(_service, _tracer);
            _coreJobPositionQuery = new CoreJobPositions(_service, _tracer);
            _coreStaffMemberQuery = new CoreStaffMembers(_service, _tracer);

            _coreJobAssignmentQuery = new CoreJobAssignments(_service, _tracer);

<<<<<<< HEAD
            if (snap.SourceOfTruthId == null)
            {
                _tracer.Trace("Has no source of truth id Core Job Assignment. Searching by STAFF JOB POSITIONS SEQ: {0}", snap.StaffJobPositionsSeq);
                coreAssignment = _coreJobAssignmentQuery.GetEntityBySeq(snap.StaffJobPositionsSeq);
            }
            else
            {
                _tracer.Trace("Has Source truth id Core Job Assignment. Searching by TRUTH");
                coreAssignment = _coreJobAssignmentQuery.GetEntityByTruth(snap.SourceOfTruthId);
            }

            _tracer.Trace("Core Assignment returned with Id: {0}", coreAssignment.Id);

            CoreJobAssignment updateCore;
            CoreJobAssignment createCore;

            if (coreAssignment.Id != Guid.Empty)
            {
                updateCore = new CoreJobAssignment
                {
                    IsCreate = false,
                    CoreJobAssignmentId = coreAssignment.Id
                };

                if (attributes.Contains(SynJobPositionFields.JobPositionDescription)
                ) MapName(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffJobPositionsSeq))
                    MapStaffJobPositionSeq(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.SourceOfTruthId))
                    MapSourceOfTrithId(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffId))
                    MapStaffId(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffId))
                    MapCoreStaffMember(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffJobPositionCode))
                    MapCoreJobPosition(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffJobPositionFTE))
                    MapAssignmentFte(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffJobPositionStartDate))
                    MapStartDate(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffJobPositionEndDate))
                    MapEndDate(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.StaffJobPositionActiveFlag))
                    MapIsActive(snap, updateCore);

                if (attributes.Contains(SynJobPositionFields.ReportsTo))
                    MapSynReportsTo(snap, updateCore);

                var json = JsonSerializer.Serialize(
                    updateCore,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                );

                _tracer.Trace("updateCore CoreJobAssignment Entity: {0}", json);

                return updateCore;
            }
            else
            {
                // create new DTO with all relevant fields
                createCore = new CoreJobAssignment
                {
                    SourceOfTruthId = snap.SourceOfTruthId,
                    IsCreate = true
                };

                MapName(snap, createCore);
                MapStaffJobPositionSeq(snap, createCore);
                MapSourceOfTrithId(snap, createCore);
                MapStaffId(snap, createCore);
                MapCoreStaffMember(snap, createCore);
                MapCoreJobPosition(snap, createCore);
                MapAssignmentFte(snap, createCore);
                MapStartDate(snap, createCore);
                MapEndDate(snap, createCore);
                MapIsActive(snap, createCore);
                MapSynReportsTo(snap, createCore);

                var json = JsonSerializer.Serialize(
                    createCore,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                );

                _tracer.Trace("createCore CoreJobAssignment Entity: {0}", json);

                return createCore;
            }
        }
        private void MapName(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.Name = snap.Name;
        }
        private void MapStaffJobPositionSeq(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.StaffJobPositionSeq = snap.StaffJobPositionsSeq;
        }
        private void MapSourceOfTrithId(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.SourceOfTruthId = snap.SourceOfTruthId;
        }
        private void MapStaffId(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.StaffId = snap.StaffId;
=======
            if (snap.StaffJobPositionsSeq != null)
            {
                _tracer.Trace("Searching by STAFF JOB POSITIONS SEQ: {0}", snap.StaffJobPositionsSeq);
                existingCoreAssignment = _coreJobAssignmentQuery.GetEntityBySeq(snap.StaffJobPositionsSeq);
            }
            if (existingCoreAssignment == null && !string.IsNullOrWhiteSpace(snap.SourceOfTruthId))
            {
                _tracer.Trace("existingCoreAssignment is still null and has Source truth id Core Job Assignment. Searching by TRUTH");
                existingCoreAssignment = _coreJobAssignmentQuery.GetEntityByTruth(snap.SourceOfTruthId!);
            }

            _create = existingCoreAssignment == null;
            if (existingCoreAssignment != null) _create = existingCoreAssignment.Id == Guid.Empty;

            if (_create == false && existingCoreAssignment != null) _tracer.Trace("Core Assignment returned with Id: {0} AND _create set to FALSE", existingCoreAssignment.Id);

            CoreJobAssignment coreAssignment;

            coreAssignment = _create == false
                ? new CoreJobAssignment
                {
                    IsCreate = false,
                    CoreJobAssignmentId = existingCoreAssignment.Id
                }
                : new CoreJobAssignment
                {
                    IsCreate = true
                };

            MapName(snap, coreAssignment, attributes);
            MapStaffJobPositionSeq(snap, coreAssignment, attributes);
            MapSourceOfTrithId(snap, coreAssignment, attributes);
            MapStaffId(snap, coreAssignment, attributes);
            MapCoreStaffMember(snap, coreAssignment);
            MapCoreJobPosition(snap, coreAssignment, attributes);
            MapAssignmentFte(snap, coreAssignment, attributes);
            MapFteRequired(snap, coreAssignment, attributes);
            MapPositionDates(snap, coreAssignment, attributes);
            MapIsActive(snap, coreAssignment, attributes);
            MapSynReportsTo(snap, coreAssignment, attributes);
            MapStatus(snap, coreAssignment, attributes);

            _tracer.Trace("coreAssignment CoreJobAssignment IsCreate: {0}, StaffId: {1}, StaffJobPositionSeq: {2}",
                coreAssignment.IsCreate,
                coreAssignment.StaffId,
                coreAssignment.StaffJobPositionSeq
            );

            return coreAssignment;
        }
        private void MapName(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.JobPositionDescription))
            {
                target.Name = snap.Name;
            }
        }
        private void MapFteRequired(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.JobPositionFTERequired))
            {
                target.FteRequired = snap.FteRequired;
            }
        }
        private void MapStaffJobPositionSeq(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.StaffJobPositionsSeq))
            {
                target.StaffJobPositionSeq = snap.StaffJobPositionsSeq;
            }
        }
        private void MapSourceOfTrithId(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.SourceOfTruthId))
            {
                target.SourceOfTruthId = snap.SourceOfTruthId;
            }
        }
        private void MapStaffId(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.StaffId))
            {
                target.StaffId = snap.StaffId;
            }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        }
        private void MapCoreStaffMember(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            Entity coreStaffMember = _coreStaffMemberQuery!.GetEntityByStaffId(snap.StaffId);

            EntityReference coreRef = new EntityReference(
                "pgrowth_corestaffmember",
                coreStaffMember.Id
            );

<<<<<<< HEAD
            target.CoreStaffMember = coreRef;
            target.CoreStaffMemberIdx = coreRef.Id.ToString();
        }
        private void MapCoreJobPosition(JobPositionSnapshot snap, CoreJobAssignment target)
=======
            if (coreStaffMember != null && coreRef != null)
            {
                target.CoreStaffMember = coreRef;
                target.CoreStaffMemberIdx = coreRef.Id.ToString();
            }
        }
        private void MapCoreJobPosition(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        {
            EntityReference corePositionRef = _coreJobPositionQuery!.GetReferenceBySeq(snap.JobPositionsSeq);

            target.CoreJobPosition = corePositionRef;
            target.CoreJobPositionIdx = corePositionRef.Id.ToString();
        }
<<<<<<< HEAD
        private void MapAssignmentFte(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.AssignmentFte = snap.AssignmentFte;
        }
        private void MapStartDate(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.StartDate = snap.StartDate;
        }
        private void MapEndDate(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.EndDate = snap.EndDate;
        }
        private void MapIsActive(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.IsActive = snap.IsActive == 0;
        }
        private void MapSynReportsTo(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            target.SynReportsTo = snap.SynReportsTo;
=======
        private void MapAssignmentFte(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.StaffJobPositionFTE))
            {
                target.AssignmentFte = snap.AssignmentFte;
            }
        }
        private void MapPositionDates(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (
                (_create || attributes.Contains(SynJobPositionFields.StaffJobPositionStartDate))
                && snap.StartDate.HasValue
            ) target.StartDate = snap.StartDate;

            if (
                (_create || attributes.Contains(SynJobPositionFields.StaffJobPositionEndDate))
                && snap.EndDate.HasValue
            ) target.EndDate = snap.EndDate;
        }
        private void MapIsActive(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            _tracer.Trace("MapIsActive: snap.IsActive: {0}, attributes.Contains(staffJobPosActiveFlag): {1}",
                snap.IsActive,
                attributes.Contains(SynJobPositionFields.StaffJobPositionActiveFlag)
            );
            if (_create || attributes.Contains(SynJobPositionFields.StaffJobPositionActiveFlag))
            {
                target.IsActive = snap.IsActive == 1;
            }
        }
        private void MapSynReportsTo(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            if (_create || attributes.Contains(SynJobPositionFields.ReportsTo))
            {
                target.SynReportsTo = snap.SynReportsTo;
            }
        }
        private void MapStatus(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            _tracer.Trace("MapStatus Called. _create: {0}, attributes.Contains(statecode): {1}, attributes.Contains(statuscode): {2}",
                _create,
                attributes.Contains("statecode"),
                attributes.Contains("statecode")
            );
            if (_create || attributes.Contains("statecode") || attributes.Contains("statuscode"))
            {
                target.Status = snap.Status;
                target.StatusReason = snap.StatusReason;
                _tracer.Trace("target.Status: {0}, target.StatusReason: {1}",
                    ((Enums.Status)target.Status.Value).ToString(),
                    ((Enums.StatusReason)target.StatusReason.Value).ToString()
                );
            }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        }
    }
}