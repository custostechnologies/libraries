using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Synergetic Job Position schema used for assignment change detection.
    /// </summary>
    public static class SynJobPositionSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            // Identity
            SynJobPositionFields.Id,
            SynJobPositionFields.RecordTitle,
            SynJobPositionFields.StaffId,
            SynJobPositionFields.SourceOfTruthId,

            // Staff Job Position
            SynJobPositionFields.StaffJobPositionActiveFlag,
            SynJobPositionFields.StaffJobPositionCode,
            SynJobPositionFields.StaffJobPositionStartDate,
            SynJobPositionFields.StaffJobPositionEndDate,
            SynJobPositionFields.StaffJobPositionExistsFlag,
            SynJobPositionFields.StaffJobPositionFTE,
            SynJobPositionFields.StaffJobPositionsSeq,

            // Hash Values
            SynJobPositionFields.LongEncodedHash,
            SynJobPositionFields.PlaintextHash,

            // Status
            SynJobPositionFields.Status,
            SynJobPositionFields.StatusReason,

            // Sync
            SynJobPositionFields.UtcNow
        );

        public static readonly HashSet<string> TrackedFields = new HashSet<string>
        {
            SynJobPositionFields.Id,
            SynJobPositionFields.RecordTitle,
            SynJobPositionFields.SourceOfTruthId,
            SynJobPositionFields.StaffId,
            SynJobPositionFields.StaffJobPositionActiveFlag,
            SynJobPositionFields.StaffJobPositionCode,
            SynJobPositionFields.StaffJobPositionStartDate,
            SynJobPositionFields.StaffJobPositionEndDate,
            SynJobPositionFields.StaffJobPositionExistsFlag,
            SynJobPositionFields.StaffJobPositionFTE,
            SynJobPositionFields.StaffJobPositionsSeq,
            "pgrowth_forceplugin"
        };
    }
}