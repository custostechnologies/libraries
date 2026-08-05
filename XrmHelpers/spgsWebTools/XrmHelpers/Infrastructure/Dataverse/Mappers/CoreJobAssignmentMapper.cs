using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public static class CoreJobAssignmentMapper
    {
        private const string ENTITY_LOGICAL_NAME = "pgrowth_corejobassignment";
        public static Entity ToCreateEntity(this CoreJobAssignment core, ITracingService tracer)
        {
            var entity = core.ToEntity(true);

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
        public static Entity ToUpdateEntity(this CoreJobAssignment core, ITracingService tracer)
        {
            if (core.CoreJobAssignmentId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = core.ToEntity(false);
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

        public static Entity ToEntity(this CoreJobAssignment core, bool create)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);

            // Identity
            if (!string.IsNullOrWhiteSpace(core.Name))
                entity[CoreJobAssignmentFields.Name] = core.Name;

            if (!string.IsNullOrWhiteSpace(core.SourceOfTruthId))
                entity[CoreJobAssignmentFields.SourceOfTruthId] = core.SourceOfTruthId;

            if (core.StaffId != null)
                entity[CoreJobAssignmentFields.StaffId] = core.StaffId;

            if (core.StaffJobPositionSeq != null)
                entity[CoreJobAssignmentFields.StaffJobPositionsSeq] = core.StaffJobPositionSeq;

            // Status
            if (core.IsActive != null)
                entity[CoreJobAssignmentFields.IsActive] = core.IsActive;

            if (core.StatusReason != null && !create)
                entity[CoreJobAssignmentFields.StatusReason] = core.StatusReason;

            // Job metadata
            if (core.CoreStaffMember != null)
                entity[CoreJobAssignmentFields.CoreStaffMember] = core.CoreStaffMember;

            if (!string.IsNullOrWhiteSpace(core.CoreStaffMemberIdx))
                entity[CoreJobAssignmentFields.CoreStaffMemberIdx] = core.CoreStaffMemberIdx;

            if (!string.IsNullOrWhiteSpace(core.CoreJobPositionIdx))
                entity[CoreJobAssignmentFields.CoreJobPositionIdx] = core.CoreJobPositionIdx;

            if (core.CoreJobPosition != null)
                entity[CoreJobAssignmentFields.CoreJobPosition] = core.CoreJobPosition;

            if (core.AssignmentFte != null)
                entity[CoreJobAssignmentFields.AssignmentFte] = core.AssignmentFte;

            if (core.StartDate.HasValue)
                entity[CoreJobAssignmentFields.StartDate] = core.StartDate.Value;

            if (core.EndDate.HasValue)
                entity[CoreJobAssignmentFields.EndDate] = core.EndDate.Value;

            if (core.FteRequired != null)
                entity[CoreJobAssignmentFields.FteRequired] = core.FteRequired;

            if (!string.IsNullOrWhiteSpace(core.SynReportsTo))
                entity[CoreJobAssignmentFields.SynReportsTo] = core.SynReportsTo;

            // Sync data
            if (core.LastSync.HasValue)
                entity[CoreJobAssignmentFields.LastSync] = core.LastSync.Value;

            return entity;
        }
    }
}