using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum RiskConsequence
    {
        [Description("Insignificant")]
        Insignificant = 1,

        [Description("Minor")]
        Minor = 2,

        [Description("Moderate")]
        Moderate = 3,

        [Description("Major")]
        Major = 4,

        [Description("Catastrophic")]
        Catastrophic = 5
    }
}