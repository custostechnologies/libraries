using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum ComplianceItemStatusReason
    {
        [Description("Compliance")]
        Compliant = 1,

        [Description("Non-Compliant")]
        NonCompliance = 386630001,

        [Description("Inactive")]
        Inactive = 2
    }
}