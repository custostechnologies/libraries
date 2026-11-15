using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum ActivityStatus
    {
        [Description("Open")]
        Open = 0,
        [Description("Completed")]
        Completed = 1,
        [Description("Canceled")]
        Canceled = 2,
        [Description("Scheduled")]
        Scheduled = 3
    }
}