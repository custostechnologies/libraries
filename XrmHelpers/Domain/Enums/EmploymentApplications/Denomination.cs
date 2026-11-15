using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum Denomination
    {
        [Description("Anglican")]
        Anglican = 386630000,
        [Description("Catholic")]
        Catholic = 386630001,
        [Description("Eastern Orthodox")]
        EasternOrthodox = 386630002,
        [Description("Protestant")]
        Protestant = 386630003,
        [Description("Lutheran")]
        Lutheran = 386630004,
        [Description("Presbyterian")]
        Presbyterian = 386630005,
        [Description("Methodist")]
        Methodist = 386630006,
        [Description("Other")]
        Other = 386630007,
        [Description("None")]
        None = 386630008
    }
}