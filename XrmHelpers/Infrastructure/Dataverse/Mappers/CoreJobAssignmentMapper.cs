using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;
using spgsWebTools.XrmHelpers.Constants;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.VisualBasic;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public class CoreJobAssignmentMapper
    {
        private readonly string ENTITY_LOGICAL_NAME = "mthomas_corejobassignment";
        private readonly IDataverseLogger _logger;
        private bool _create = false;
        public CoreJobAssignmentMapper(IDataverseLogger logger)
        {
            _logger = logger;
        }
        public Entity ToCreateEntity(CoreJobAssignment core, IDataverseLogger logger, string[] updatedAttributes)
        {
            var entity = ToEntity(core, true, updatedAttributes);

            foreach (var a in entity.Attributes)
            {
                logger.WriteLog(
                    "{0} = {1}",
                    a.Key,
                    a.Value?.ToString() ?? "<null>"
                );
            }

            return entity;
        }
        public Entity ToUpdateEntity(CoreJobAssignment core, IDataverseLogger logger, string[] updatedAttributes)
        {
            if (core == null || core.CoreJobAssignmentId == Guid.Empty)
            {
                throw new InvalidPluginExecutionException("Can't build update entity without existing record.");
            }

            var entity = ToEntity(core, false, updatedAttributes);
            entity.Id = core.CoreJobAssignmentId;

            foreach (var a in entity.Attributes)
            {
                logger.WriteLog(
                    "{0} = {1}",
                    a.Key,
                    a.Value?.ToString() ?? "<null>"
                );
            }

            return entity;
        }

        public Entity ToEntity(CoreJobAssignment core, bool create, string[] updatedAttributes)
        {
            var entity = new Entity(ENTITY_LOGICAL_NAME);
            _create = create;

            // Identity
            MapDelta(
                entity,
                updatedAttributes,
                SynJobPositionFields.RecordTitle,
                CoreJobAssignmentFields.Name,
                core.Name
            );

            if (!string.IsNullOrWhiteSpace(core.SourceOfTruthId) &&
                updatedAttributes.Contains("pgrowth_sourceoftruthid"))
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
            MapIsActiveDelta(
                entity,
                updatedAttributes,
                core.IsActive ?? false
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

            _logger.WriteLog("entity has status: {0}, statusReason: {1}",
                ((Enums.Status)entityStatus.Value).ToString(),
                ((Enums.StatusReason)entityStatusReason.Value).ToString()
            );

            // Sync data
            entity[CoreJobAssignmentFields.LastSync] = DateTime.UtcNow;

            return entity;
        }
        private void MapDelta<T>(
            Entity entity,
            string[] updatedAttributes,
            string sourceAttribute,
            string targetAttribute,
            T value
        )
        {
            _logger.WriteLog("MapDelta srcAttribute: {0}, targetAttribute: {1}, value: {2}",
                sourceAttribute, targetAttribute, value
            );
            if (_create || updatedAttributes.Contains(sourceAttribute))
            {
                entity[targetAttribute] = value;
            }
        }
        private void MapIsActiveDelta(
            Entity entity,
            string[] updatedAttributes,
            bool value
        )
        {
            _logger.WriteLog("MapDelta srcAttribute: {0}, targetAttribute: {1}, value: {2}",
            SynJobPositionFields.StaffJobPositionActiveFlag, CoreJobAssignmentFields.IsActive, value
            );
            if (_create || updatedAttributes.Contains(SynJobPositionFields.StaffJobPositionActiveFlag))
            {
                entity[CoreJobAssignmentFields.IsActive] = value;
                _logger.WriteLog("Set CoreJobAssignmentFields.IsActive to {0}", entity.GetAttributeValue<bool>(CoreJobAssignmentFields.IsActive));
            }
        }
    }
}