using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum StatusReason
    {
        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 2
    }
}