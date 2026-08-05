using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.NewUserSyncEvent.Factories
{
    public class CoreJobAssignmentFactory
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private CoreJobAssignments? _coreAssignmentQuery;
        private CoreJobPositions? _corePositionQuery;
        private CoreStaffMembers? _coreStaffQuery;
        public CoreJobAssignmentFactory(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public CoreJobAssignment Build(JobPositionSnapshot snap, string[] attributes)
        {
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
            _coreAssignmentQuery = new CoreJobAssignments(_service, _tracer);

            if (snap.SourceOfTruthId == null)
            {
                _tracer.Trace("Unable to resolve Core Job Assignment.");
                throw new Exception("Core Job Assignment Resolution Failed.");
            }
            Entity coreAssignment = _coreAssignmentQuery.GetEntityByTruth(snap.SourceOfTruthId);

            CoreJobAssignment updateCore;
            CoreJobAssignment createCore;

            if (coreAssignment.Id == Guid.Empty)
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
        }
        private void MapCoreStaffMember(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            _coreStaffQuery = new CoreStaffMembers(_service, _tracer);

            EntityReference coreStaffRef = _coreStaffQuery.GetEntityReferenceByStaffId(snap.StaffId);

            target.CoreStaffMember = coreStaffRef;
            target.CoreStaffMemberIdx = coreStaffRef.Id.ToString();
        }
        private void MapCoreJobPosition(JobPositionSnapshot snap, CoreJobAssignment target)
        {
            _corePositionQuery = new CoreJobPositions(_service, _tracer);

            EntityReference corePositionRef = _corePositionQuery.GetReferenceBySeq(snap.JobPositionsSeq);

            target.CoreJobPosition = corePositionRef;
            target.CoreJobPositionIdx = corePositionRef.Id.ToString();
        }
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
        }
    }
}