using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum StatusReason
    {
        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 2
    }
}