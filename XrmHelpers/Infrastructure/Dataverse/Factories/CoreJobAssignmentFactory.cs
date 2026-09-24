using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories
{
    public class CoreJobAssignmentFactory
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        private CoreJobAssignments? _coreJobAssignmentQuery;
        private CoreJobPositions? _coreJobPositionQuery;
        private CoreStaffMembers? _coreStaffMemberQuery;
        private bool _create = false;
        public CoreJobAssignmentFactory(IDataverseContext context, DataverseLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public CoreJobAssignment Build(JobPositionSnapshot snap, string[] attributes)
        {
            Entity existingCoreAssignment = null;

            _logger.WriteLog("Entered CoreJobAssignmentFactory.Build");
            _logger.WriteLog("Initialising query obejcts");

            _coreJobAssignmentQuery = new CoreJobAssignments(_context);
            _coreJobPositionQuery = new CoreJobPositions(_context, _logger);
            _coreStaffMemberQuery = new CoreStaffMembers(_context, _logger);

            _coreJobAssignmentQuery = new CoreJobAssignments(_context);

            if (snap.StaffJobPositionsSeq != null)
            {
                _logger.WriteLog("Searching by STAFF JOB POSITIONS SEQ: {0}", snap.StaffJobPositionsSeq);
                existingCoreAssignment = _coreJobAssignmentQuery.GetEntityBySeq(snap.StaffJobPositionsSeq);
            }
            if (existingCoreAssignment == null && !string.IsNullOrWhiteSpace(snap.SourceOfTruthId))
            {
                _logger.WriteLog("existingCoreAssignment is still null and has Source truth id Core Job Assignment. Searching by TRUTH");
                existingCoreAssignment = _coreJobAssignmentQuery.GetEntityByTruth(snap.SourceOfTruthId!);
            }

            _create = existingCoreAssignment == null;
            if (existingCoreAssignment != null) _create = existingCoreAssignment.Id == Guid.Empty;

            if (_create == false && existingCoreAssignment != null) _logger.WriteLog("Core Assignment returned with Id: {0} AND _create set to FALSE", existingCoreAssignment.Id);

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

            _logger.WriteLog("coreAssignment CoreJobAssignment IsCreate: {0}, StaffId: {1}, StaffJobPositionSeq: {2}",
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
        }
        private void MapCoreStaffMember(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            Entity coreStaffMember = _coreStaffMemberQuery!.GetEntityByStaffId(snap.StaffId);

            EntityReference coreRef = new EntityReference(
                "pgrowth_corestaffmember",
                coreStaffMember.Id
            );

            if (coreStaffMember != null && coreRef != null)
            {
                target.CoreStaffMember = coreRef;
                target.CoreStaffMemberIdx = coreRef.Id.ToString();
            }
        }
        private void MapCoreJobPosition(JobPositionSnapshot snap, CoreJobAssignment target, string[] attributes)
        {
            EntityReference corePositionRef = _coreJobPositionQuery!.GetReferenceBySeq(snap.JobPositionsSeq);

            target.CoreJobPosition = corePositionRef;
            target.CoreJobPositionIdx = corePositionRef.Id.ToString();
        }
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
            _logger.WriteLog("MapIsActive: snap.IsActive: {0}, attributes.Contains(staffJobPosActiveFlag): {1}",
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
            _logger.WriteLog("MapStatus Called. _create: {0}, attributes.Contains(statecode): {1}, attributes.Contains(statuscode): {2}",
                _create,
                attributes.Contains("statecode"),
                attributes.Contains("statecode")
            );
            if (_create || attributes.Contains("statecode") || attributes.Contains("statuscode"))
            {
                target.Status = snap.Status;
                target.StatusReason = snap.StatusReason;
                _logger.WriteLog("target.Status: {0}, target.StatusReason: {1}",
                    ((Enums.Status)target.Status.Value).ToString(),
                    ((Enums.StatusReason)target.StatusReason.Value).ToString()
                );
            }
        }
    }
}