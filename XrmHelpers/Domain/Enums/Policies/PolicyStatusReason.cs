using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum PolicyStatusReason
    {
        [Description("In Force")]
        InForce = 1,
        [Description("Under Review")]
        UnderReview = 386630001,
        [Description("Superseded")]
        Superseded = 2
    }
}