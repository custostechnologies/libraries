using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum ActivityStatusReason
    {
        [Description("Not Started")]
        NotStarted = 2,
        [Description("In Progress")]
        InProgress = 3,
        [Description("Waiting on someone else")]
        WaitingOnSomeoneElse = 4,
        [Description("Deferred")]
        Deferred = 7,

        [Description("Completed")]
        Completed = 5,

        [Description("Canceled")]
        Canceled = 6
    }
}