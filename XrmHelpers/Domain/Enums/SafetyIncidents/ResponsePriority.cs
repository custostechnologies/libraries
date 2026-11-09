using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum ResponsePriority
    {
        [Description("Immediate")]
        Immediate = 386630000,
        [Description("Urgent")]
        Urgent = 386630001,
        [Description("Priority")]
        Priority = 386630002,
        [Description("Routine")]
        Routine = 386630003
    }
}