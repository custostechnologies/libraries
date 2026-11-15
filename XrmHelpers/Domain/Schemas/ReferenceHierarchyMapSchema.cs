using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Position schema used for assignment change detection.
    /// </summary>
    public static class ReferenceHierarchyMapSchema
    {
        public static readonly ColumnSet AllSyncColumns = new ColumnSet(
            ReferenceHierarchyMapFields.ReferenceHierarchyMapId,
            ReferenceHierarchyMapFields.ChildJobPosition,
            ReferenceHierarchyMapFields.CoreJobPosition,
            ReferenceHierarchyMapFields.LinkType,
            ReferenceHierarchyMapFields.MapVersion,
            ReferenceHierarchyMapFields.Name,
            ReferenceHierarchyMapFields.ParentJobPosition,
            ReferenceHierarchyMapFields.SourceOfTruthId,
            ReferenceHierarchyMapFields.Status,
            ReferenceHierarchyMapFields.StatusReason,
            ReferenceHierarchyMapFields.ApprovalIndex
        );
        public static readonly ColumnSet CoreIdentityColumns = new ColumnSet(
            ReferenceHierarchyMapFields.ReferenceHierarchyMapId,
            ReferenceHierarchyMapFields.SourceOfTruthId
        );
    }
}