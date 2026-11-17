using System.ComponentModel;

using Custos.XrmHelpers.Domain.Attributes;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum ResponsePriority
{
    [Description("Immediate")]
    [EnumColour("#7C3AED")]
    Immediate = 386630000,

    [Description("Urgent")]
    [EnumColour("#2563EB")]
    Urgent = 386630001,

    [Description("Priority")]
    [EnumColour("#0891B2")]
    Priority = 386630002,

    [Description("Routine")]
    [EnumColour("#64748B")]
    Routine = 386630003
}
}