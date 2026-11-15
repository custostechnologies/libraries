using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Operational Hierarchy Map schema used for assignment change detection.
    /// </summary>
    public static class OperationalHierarchyMapSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            OperationalHierarchyMapFields.OperationalHierarchyMapId,

            OperationalHierarchyMapFields.ChildJobAssignment,
            OperationalHierarchyMapFields.ChildJobAssignmentIdx,

            OperationalHierarchyMapFields.ChildJobPosition,
            OperationalHierarchyMapFields.ChildJobPositionIdx,

            OperationalHierarchyMapFields.CoreJobAssignment,
            OperationalHierarchyMapFields.CoreJobAssignmentIdx,

            OperationalHierarchyMapFields.CoreJobPosition,
            OperationalHierarchyMapFields.CoreJobPositionIdx,

            OperationalHierarchyMapFields.CreatedFromStaffJobPositionSeq,

            OperationalHierarchyMapFields.IsActive,

            OperationalHierarchyMapFields.LinkType,

            OperationalHierarchyMapFields.Name,

            OperationalHierarchyMapFields.ParentJobAssignment,
            OperationalHierarchyMapFields.ParentJobAssignmentIdx,

            OperationalHierarchyMapFields.ParentJobPosition,
            OperationalHierarchyMapFields.ParentJobPositionIdx,

            OperationalHierarchyMapFields.SourceOfTruthId,
            OperationalHierarchyMapFields.Status,
            OperationalHierarchyMapFields.StatusReason
        );
    }
}