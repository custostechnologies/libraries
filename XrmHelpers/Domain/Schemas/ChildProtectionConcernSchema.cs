using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Assignment schema used for synchronisation operations.
    /// </summary>
    public static class ChildProtectionConcernSchema
    {
        public static readonly ColumnSet DigestColumns = new ColumnSet(
            ChildProtectionConcernFields.Name,
            ChildProtectionConcernFields.StudentType,
            ChildProtectionConcernFields.StudentName,
            ChildProtectionConcernFields.ConcernType,
            ChildProtectionConcernFields.Flag,
            ChildProtectionConcernFields.SubmitterEmail,
            ChildProtectionConcernFields.SubmitterName,
            ChildProtectionConcernFields.Status,
            ChildProtectionConcernFields.StatusReason,
            "ownerid"
        );
    }
}