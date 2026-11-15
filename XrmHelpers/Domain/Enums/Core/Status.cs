using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum Status
    {
        [Description("Active")]
        Active = 0,
        [Description("Inactive")]
        Inactive = 1
    }
}