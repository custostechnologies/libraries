using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Pay Claim Form schema used for synchronisation operations.
    /// </summary>
    public static class PayClaimItemSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            // Identity
            PayClaimItemFields.PayClaimItemId,
            PayClaimItemFields.Date,
            PayClaimItemFields.StartTime,
            PayClaimItemFields.FinishTime,
            PayClaimItemFields.BreakLength,
            PayClaimItemFields.CalcDailyClockedHours,
            PayClaimItemFields.CalcDailyPayableHours,
            PayClaimItemFields.DailyClockedHours,
            PayClaimItemFields.DailyPayableHours,
            PayClaimItemFields.Status,
            PayClaimItemFields.StatusReason,
            PayClaimItemFields.PayClaimForm,
            PayClaimItemFields.CreationId,
            PayClaimItemFields.ChildId
        );
    }
}