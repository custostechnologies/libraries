using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Assignment schema used for synchronisation operations.
    /// </summary>
    public static class ChildProtectionConcernSchema
    {
        public static readonly ColumnSet DigestColumns = new ColumnSet(
            ChildProtectionConcernFields.AutoGenTitle,
            ChildProtectionConcernFields.StudentType,
            ChildProtectionConcernFields.StudentName,
            ChildProtectionConcernFields.ConcernTypes,
            ChildProtectionConcernFields.Flag,
            ChildProtectionConcernFields.SubmitterEmail,
            ChildProtectionConcernFields.SubmitterName,
            ChildProtectionConcernFields.Status,
            ChildProtectionConcernFields.StatusReason,
            ChildProtectionConcernFields.ChildProtectionConcernId,
            "ownerid"
        );
    }
}