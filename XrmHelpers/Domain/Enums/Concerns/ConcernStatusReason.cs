using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum ConcernStatusReason
    {
        [Description("Open")]
        Open = 1,
        [Description("Draft")]
        Draft = 386630001,
        [Description("Closed")]
        Closed = 2
    }
}