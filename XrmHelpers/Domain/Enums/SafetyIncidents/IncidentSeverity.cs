using System.ComponentModel;

using Custos.XrmHelpers.Domain.Attributes;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum IncidentSeverity
    {
        [Description("Critical")]
        [EnumColour("#D92D20")]
        Critical = 386630000,

        [Description("High")]
        [EnumColour("#DC6803")]
        High = 386630001,

        [Description("Medium")]
        [EnumColour("#CA8A04")]
        Medium = 386630002,

        [Description("Low")]
        [EnumColour("#2563EB")]
        Low = 386630003,

        [Description("Negligible")]
        [EnumColour("#64748B")]
        Negligible = 386630004
    }
}