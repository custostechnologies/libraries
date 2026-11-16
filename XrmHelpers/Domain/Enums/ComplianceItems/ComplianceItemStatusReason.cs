using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum ComplianceItemStatusReason
    {
        [Description("Compliance")]
        Compliant = 1,

        [Description("Non-Compliant")]
        NonCompliant = 386630001,

        [Description("Inactive")]
        Inactive = 2
    }
}