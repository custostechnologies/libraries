using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum HazardStatusReason
    {
        [Description("Active")]
        Active = 1,
        [Description("Under Review")]
        UnderReview = 386630001,
        [Description("Inactive")]
        Inactive = 2
    }
}