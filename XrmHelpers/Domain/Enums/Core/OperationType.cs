using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum OperationType
    {
        [Description("Create")]
        Create = 0,
        [Description("Update Owner")]
        UpdateOwner = 1,
        [Description("Update Field")]
        UpdateField = 2,
        [Description("Close Record")]
        CloseRecord = 3,
        [Description("Summary Notification")]
        SummaryNotification = 4,
        [Description("None")]
        None = 5
    }
}