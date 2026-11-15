using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum SyncStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Validated")]
        Validated = 2,
        [Description("Mapped")]
        Mapped = 3,
        [Description("Created")]
        Created = 4,
        [Description("Updated")]
        Updated = 5,
        [Description("Failed")]
        Failed = 6,
        [Description("Skipped")]
        Skipped = 7
    }
}