using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Pay Claim Form schema used for synchronisation operations.
    /// </summary>
    public static class PayPeriodSchema
    {
        public static readonly ColumnSet MetadataColumns = new ColumnSet(
            // Identity
            PayPeriodFields.PayPeriodId,
            PayPeriodFields.Status,
            PayPeriodFields.Name,
            PayPeriodFields.CasualCurrentFlag,
            PayPeriodFields.CasualPeriodEnd,
            PayPeriodFields.CasualPeriodStart,
            PayPeriodFields.SalaryPeriodEnd,
            PayPeriodFields.SalaryPeriodStart,
            PayPeriodFields.ProcessedDate,
            PayPeriodFields.WeekNumber
        );
    }
}