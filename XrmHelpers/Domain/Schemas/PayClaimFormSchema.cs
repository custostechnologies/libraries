using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Pay Claim Form schema used for synchronisation operations.
    /// </summary>
    public static class PayClaimFormSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            // Identity
            PayClaimFormFields.PayClaimFormId,
            PayClaimFormFields.StaffMember,
            PayClaimFormFields.FormType,
            PayClaimFormFields.PayPeriod,
            PayClaimFormFields.RelatedApprovalId,
            PayClaimFormFields.RespondentType,
            PayClaimFormFields.Status,
            PayClaimFormFields.StatusReason,
            PayClaimFormFields.Title,
            PayClaimFormFields.TotalClockedHours,
            PayClaimFormFields.TotalPayableHours,
            PayClaimFormFields.TotalUnpaidBreakHours
        );
    }
}