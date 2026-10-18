using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Task schema used for digest and notification operations.
    /// </summary>
    public static class TaskSchema
    {
        public static readonly ColumnSet BaseColumns = new ColumnSet(
            TaskFields.TaskId,
            TaskFields.Subject,
            TaskFields.Description,

            // Scheduling
            TaskFields.ScheduledStart,
            TaskFields.ScheduledEnd,
            TaskFields.ScheduledDuration,

            // Progress / Status
            TaskFields.Status,
            TaskFields.StatusReason,
            TaskFields.PercentComplete,
            TaskFields.Priority,

            // Ownership
            TaskFields.Owner,
            TaskFields.OwningUser,
            TaskFields.OwningTeam,

            // Relationships
            TaskFields.Regarding,

            // Audit
            TaskFields.CreatedOn,
            TaskFields.ModifiedOn,

            // Custom Category Metadata
            TaskCustomFields.TaskCategory,
            TaskCustomFields.TaskSubCategory,
            TaskCustomFields.TaskType
        );
        public static readonly ColumnSet ComplianceItemColumns = new ColumnSet(
            // Custom Fields
            TaskCustomFields.ComplianceFlag,
            TaskCustomFields.ComplianceItem,
            
            TaskCustomFields.RecurrenceCycle,
            TaskCustomFields.TaskCreationFlowStatus
        );
    }
}