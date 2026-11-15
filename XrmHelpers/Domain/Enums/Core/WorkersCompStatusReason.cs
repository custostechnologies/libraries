using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum WorkersCompStatusReason
    {
        [Description("Open")]
        Open = 1,
        [Description("Lodged")]
        Lodged = 386630001,
        [Description("Approved")]
        Approved = 2,
        [Description("Declined")]
        Declined = 386630002,
        [Description("Canceled")]
        Canceled = 3,
        [Description("Scheduled")]
        Scheduled = 4
    }
}