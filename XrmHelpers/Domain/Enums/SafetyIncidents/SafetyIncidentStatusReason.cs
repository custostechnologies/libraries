using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum SafetyIncidentStatusReason
    {
        [Description("Open")]
        Open = 1,
        [Description("Draft")]
        Draft = 386630001,
        [Description("Closed")]
        Closed = 2
    }
}