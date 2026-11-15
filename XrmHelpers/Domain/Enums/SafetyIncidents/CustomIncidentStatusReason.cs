using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum CustomIncidentStatusReason
    {
        [Description("Draft")]
        Draft = 1,
        [Description("Submitted")]
        Submitted = 386630001,
        [Description("Inactive")]
        Inactive = 2
    }
}