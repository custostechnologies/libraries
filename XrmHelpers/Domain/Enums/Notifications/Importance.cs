using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum Importance
    {
        [Description("Low")]
        Low = 0,
        [Description("Normal")]
        Normal = 1,
        [Description("High")]
        High = 2
    }
}