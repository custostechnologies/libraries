using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;
<<<<<<< HEAD

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public static class CoreJobAssignmentMapper
    {
        private const string ENTITY_LOGICAL_NAME = "mthomas_corejobassignment";
        public static Entity ToCreateEntity(this CoreJobAssignment core, ITracingService tracer, string[] updatedAttributes)
        {
            var entity = core.ToEntity(true, updatedAttributes);
=======
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.VisualBasic;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class CoreJobAssignmentMapper
    {
        private readonly string ENTITY_LOGICAL_NAME = "mthomas_corejobassignment";
        private readonly ITracingService _tracer;
        private bool _create = false;
        public CoreJobAssignmentMapper(ITracingService tracer)
        {
            _tracer = tracer;
        }
        public Entity ToCreateEntity(CoreJobAssignment core, ITracingService tracer, string[] updatedAttributes)
        {
            var entity = ToEntity(core, true, updatedAttributes);
>>>>>>> source-temp/refactor/decouple-xrmhelpers

            foreach (var a in entity.Attributes)
            {
                tracer.Trace(
                    "{0} = {1}",
                    a.Key,
                    a.Value?.ToString() ?? "<null>"
                );
            }

            return entity;
        }
<<<<<<< HEAD
        public static Entity ToUpdateEntity(this CoreJobAssignment core, ITracingService tracer, string[] updatedAttributes)
        {
            if (core.CoreJobAssignmentId == Guid.Empty)
=======
        public Entity ToUpdateEntity(CoreJobAssignment core, ITracingService tracer, string[] updatedAttributes)
        {
            if (core == null || core.CoreJobAssignmentId == Guid.Empty)
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

<<<<<<< HEAD
            var entity = core.ToEntity(false, updatedAttributes);
=======
            var entity = ToEntity(core, false, updatedAttributes);
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            entity.Id = core.CoreJobAssignmentId;

            foreach (var a in entity.Attributes)
            {
                tracer.Trace(
                    "{0} = {1}",
                    a.Key,
                    a.Value?.ToString() ?? "<null>"
                );
            }

            return entity;
        }

<<<<<<< HEAD
        public static Entity ToEntity(this CoreJobAssignment core, bool create, string[] updatedAttributes)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);

            // Identity

=======
        public Entity ToEntity(CoreJobAssignment core, bool create, string[] updatedAttributes)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);
            _create = create;

            // Identity
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.RecordTitle,
                CoreJobAssignmentFields.Name,
                core.Name
            );

            if (!string.IsNullOrWhiteSpace(core.SourceOfTruthId) &&
<<<<<<< HEAD
    updatedAttributes.Contains("pgrowth_sourceoftruthid"))
=======
                updatedAttributes.Contains("pgrowth_sourceoftruthid"))
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            {
                entity[CoreStaffMemberFields.SourceOfTruthId] = core.SourceOfTruthId;
            }

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StaffId,
                CoreJobAssignmentFields.StaffId,
                core.StaffId
            );

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StaffJobPositionsSeq,
                CoreJobAssignmentFields.StaffJobPositionSeq,
                core.StaffJobPositionSeq
            );

            // Status
<<<<<<< HEAD
            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StaffJobPositionActiveFlag,
                CoreJobAssignmentFields.IsActive,
                core.IsActive
=======
            MapIsActiveDelta(
                entity,
                updatedAttributes,
                core.IsActive ?? false
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            );

            if (core.StatusReason != null && !create)
            {
                entity[CoreJobAssignmentFields.StatusReason] = core.StatusReason;
            }

            // Job metadata
            if (core.CoreStaffMember != null)
                entity[CoreJobAssignmentFields.CoreStaffMember] = core.CoreStaffMember;

            if (!string.IsNullOrWhiteSpace(core.CoreStaffMemberIdx))
                entity[CoreJobAssignmentFields.CoreStaffMemberIdx] = core.CoreStaffMemberIdx;

            if (!string.IsNullOrWhiteSpace(core.CoreJobPositionIdx))
                entity[CoreJobAssignmentFields.CoreJobPositionIdx] = core.CoreJobPositionIdx;

            if (core.CoreJobPosition != null)
                entity[CoreJobAssignmentFields.CoreJobPosition] = core.CoreJobPosition;

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StaffJobPositionFTE,
                CoreJobAssignmentFields.AssignmentFte,
                core.AssignmentFte
            );

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StaffJobPositionStartDate,
                CoreJobAssignmentFields.StartDate,
                core.StartDate
            );

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StaffJobPositionEndDate,
                CoreJobAssignmentFields.EndDate,
                core.EndDate
            );

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.JobPositionFTERequired,
                CoreJobAssignmentFields.FteRequired,
                core.FteRequired
            );

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.ReportsTo,
                CoreJobAssignmentFields.SynReportsTo,
                core.SynReportsTo
            );

<<<<<<< HEAD
=======
            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.Status,
                CoreJobAssignmentFields.Status,
                core.Status
            );

            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.StatusReason,
                CoreJobAssignmentFields.StatusReason,
                core.StatusReason
            );

            var entityStatus = entity.GetAttributeValue<OptionSetValue>("statecode");
            var entityStatusReason = entity.GetAttributeValue<OptionSetValue>("statuscode");

            _tracer.Trace("entity has status: {0}, statusReason: {1}",
                ((Enums.Status)entityStatus.Value).ToString(),
                ((Enums.StatusReason)entityStatusReason.Value).ToString()
            );

>>>>>>> source-temp/refactor/decouple-xrmhelpers
            // Sync data
            entity[CoreJobAssignmentFields.LastSync] = DateTime.UtcNow;

            return entity;
        }
<<<<<<< HEAD
        private static void MapDelta<T>(
=======
        private void MapDelta<T>(
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            Entity entity,
            string[] updatedAttributes,
            string sourceAttribute,
            string targetAttribute,
            T value
        )
        {
<<<<<<< HEAD
            if (updatedAttributes.Contains(sourceAttribute))
=======
            _tracer.Trace("MapDelta srcAttribute: {0}, targetAttribute: {1}, value: {2}",
                sourceAttribute, targetAttribute, value
            );
            if (_create || updatedAttributes.Contains(sourceAttribute))
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            {
                entity[targetAttribute] = value;
            }
        }
<<<<<<< HEAD
=======
        private void MapIsActiveDelta(
            Entity entity,
            string[] updatedAttributes,
            bool value
        )
        {
            _tracer.Trace("MapDelta srcAttribute: {0}, targetAttribute: {1}, value: {2}",
            SynJobPositionFields.StaffJobPositionActiveFlag, CoreJobAssignmentFields.IsActive, value
            );
            if (_create || updatedAttributes.Contains(SynJobPositionFields.StaffJobPositionActiveFlag))
            {
                entity[CoreJobAssignmentFields.IsActive] = value;
                _tracer.Trace("Set CoreJobAssignmentFields.IsActive to {0}", entity.GetAttributeValue<bool>(CoreJobAssignmentFields.IsActive));
            }
        }
>>>>>>> source-temp/refactor/decouple-xrmhelpers
    }
}