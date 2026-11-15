using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum StateTerritory
    {
        [Description("NSW")]
        NSW = 386630000,
        [Description("VIC")]
        VIC = 386630001,
        [Description("QLD")]
        QLD = 386630002,
        [Description("SA")]
        SA = 386630003,
        [Description("NT")]
        NT = 386630004,
        [Description("WA")]
        WA = 386630005,
        [Description("TAS")]
        TAS = 386630006,
        [Description("Other")]
        Other = 386630007
    }
}