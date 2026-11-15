using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum TaskPriority
    {
        [Description("Low")]
        Low = 0,
        [Description("Medium")]
        Medium = 1,
        [Description("High")]
        High = 2
    }
}