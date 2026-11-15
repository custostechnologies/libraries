using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum NotificationType
    {
        [Description("Email")]
        Email = 384810000,
        [Description("Teams Adaptive Card")]
        TeamsAdaptiveCard = 384810001
    }
}