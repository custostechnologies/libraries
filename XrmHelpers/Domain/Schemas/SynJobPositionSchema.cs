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

            // Job Position
            SynJobPositionFields.JobPositionDescription,
            SynJobPositionFields.JobPositionFTERequired,
            SynJobPositionFields.JobPositionsSeq,

            // Staff Job Position
            SynJobPositionFields.StaffJobPositionActiveFlag,
            SynJobPositionFields.StaffJobPositionCode,
            SynJobPositionFields.StaffJobPositionStartDate,
            SynJobPositionFields.StaffJobPositionEndDate,
            SynJobPositionFields.StaffJobPositionExistsFlag,
            SynJobPositionFields.StaffJobPositionFTE,
            SynJobPositionFields.StaffJobPositionsSeq,
<<<<<<< HEAD
=======
            SynJobPositionFields.ReportsTo,
>>>>>>> source-temp/refactor/decouple-xrmhelpers

            // Hash Values
            SynJobPositionFields.EncodedHash,
            SynJobPositionFields.PlaintextHash,

            // Status
            SynJobPositionFields.Status,
            SynJobPositionFields.StatusReason,

            // Sync
<<<<<<< HEAD
            SynJobPositionFields.UtcNow
=======
            SynJobPositionFields.UtcNow,
            SynJobPositionFields.MutePlugin
>>>>>>> source-temp/refactor/decouple-xrmhelpers
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
<<<<<<< HEAD
=======
            SynJobPositionFields.ReportsTo,
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            "pgrowth_forceplugin"
        };
    }
}