using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Position schema used for assignment change detection.
    /// </summary>
    public static class CoreJobPositionSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            // Identity
            CoreJobPositionFields.CoreJobPositionId,
            CoreJobPositionFields.JobTitle,
            CoreJobPositionFields.SourceOfTruthId,
            CoreJobPositionFields.JobPositionSeq,

            // Job Details
            CoreJobPositionFields.Code,
            CoreJobPositionFields.Category,
            CoreJobPositionFields.CategoryCode,
            CoreJobPositionFields.CmeaType,
            CoreJobPositionFields.ReportsTo,

            // Status
            CoreJobPositionFields.IsActive,
            CoreJobPositionFields.Status,
            CoreJobPositionFields.StatusReason,

            // Metadata
            CoreJobPositionFields.FteRequired,
            CoreJobPositionFields.NextReviewDate,
            CoreJobPositionFields.HierarchyDependsOnCampus
        );
    }
}