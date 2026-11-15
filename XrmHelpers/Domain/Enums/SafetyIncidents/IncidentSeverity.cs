using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum IncidentSeverity
    {
        [Description("Critical")]
        Critical = 386630000,
        [Description("High")]
        High = 386630001,
        [Description("Medium")]
        Medium = 386630002,
        [Description("Low")]
        Low = 386630003,
        [Description("Negligible")]
        Negligible = 386630004
    }
}