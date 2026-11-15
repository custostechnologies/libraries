using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Assignment schema used for synchronisation operations.
    /// </summary>
    public static class CoreJobAssignmentSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            // Identity
            CoreJobAssignmentFields.CoreJobAssignmentId,
            CoreJobAssignmentFields.Name,
            CoreJobAssignmentFields.SourceOfTruthId,
            CoreJobAssignmentFields.StaffJobPositionSeq,

            // Job Position
            CoreJobAssignmentFields.CoreJobPosition,
            CoreJobAssignmentFields.CoreJobPositionIdx,
            CoreJobAssignmentFields.SynReportsTo,

            // User
            CoreJobAssignmentFields.CoreStaffMember,
            CoreJobAssignmentFields.CoreStaffMemberIdx,
            CoreJobAssignmentFields.StaffId,

            // Status
            CoreJobAssignmentFields.IsActive,
            CoreJobAssignmentFields.Status,
            CoreJobAssignmentFields.StatusReason,

            // Metadata
            CoreJobAssignmentFields.AssignmentFte,
            CoreJobAssignmentFields.FteRequired,
            CoreJobAssignmentFields.StartDate,
            CoreJobAssignmentFields.EndDate,

            CoreJobAssignmentFields.LastSync
        );
    }
}