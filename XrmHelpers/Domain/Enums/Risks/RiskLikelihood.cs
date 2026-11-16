using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum RiskLikelihood
    {
        [Description("Rare")]
        Rare = 1,

        [Description("Unlikely")]
        Unlikely = 2,

        [Description("Possible")]
        Possible = 3,

        [Description("Likely")]
        Likely = 4,

        [Description("Almost Certain")]
        AlmostCertain = 5
    }
}