using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum SyncOperationStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Started")]
        Started = 2,
        [Description("Succeeded")]
        Succeeded = 3,
        [Description("Failed")]
        Failed = 4,
        [Description("Skipped")]
        Skipped = 5
    }
}