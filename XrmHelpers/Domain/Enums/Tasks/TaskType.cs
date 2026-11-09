using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum TaskType
    {
        [Description("Policy Review")]
        PolicyReview = 386630000,
        [Description("Risk Review")]
        RiskReview = 386630001,
        [Description("Hazard Review")]
        HazardReview = 386630002,
        [Description("Compliance Action")]
        ComplianceAction = 386630003,
        [Description("Other")]
        Other = 386630004,
        [Description("Staff Policy Review")]
        StaffPolicyReview = 386630005,
        [Description("Request Review")]
        RequestReview = 386630006,
    }
}