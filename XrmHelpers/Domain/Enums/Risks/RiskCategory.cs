using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum RiskCategory
    {
        [Description("Strategic Risks")]
        StrategicRisks = 386630000,

        [Description("Regulatory Risks")]
        RegulatoryRisks = 386630001,

        [Description("Stakeholder Risks")]
        StakeholderRisks = 386630002,

        [Description("Operational Risks")]
        OperationalRisks = 386630003,

        [Description("Financial Risks")]
        FinancialRisks = 386630004,

        [Description("Educational Risks")]
        EducationalRisks = 386630005,

        [Description("Human Resources Risks")]
        HumanResourcesRisks = 386630006,

        [Description("Health and Safety Risks")]
        HealthAndSafetyRisks = 386630007
    }
}