using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum SyncEventStatusReason
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Error")]
        Error = 386630001,
        [Description("Complete")]
        Complete = 2
    }
}