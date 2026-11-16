using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum RiskRating
    {
        [Description("Low")]
        Low = 386630000,

        [Description("Medium")]
        Medium = 386630001,

        [Description("High")]
        High = 386630002,

        [Description("Extreme")]
        Extreme = 386630003
    }
}