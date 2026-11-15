using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum CMEAType
    {
        [Description("Teaching")]
        Teaching = 386630000,
        [Description("PAOS")]
        PAOS = 386630001,
        [Description("Not Applicable")]
        NotApplicable = 386630002
    }
}