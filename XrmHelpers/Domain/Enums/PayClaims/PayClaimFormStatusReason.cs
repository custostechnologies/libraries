using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum PayClaimFormStatusReason
    {
        [Description("Submitted")]
        Submitted = 386630001,
        [Description("Complete")]
        Complete = 386630002,
        [Description("Active")]
        Active = 1,
        [Description("Draft")]
        Draft = 386630004,
        [Description("Rejected")]
        Rejected = 386630003,
        [Description("Inactive")]
        Inactive = 2
    }
}