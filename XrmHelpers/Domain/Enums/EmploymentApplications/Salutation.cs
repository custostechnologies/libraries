using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum Salutation
    {
        [Description("Mr")]
        Mr = 386630000,
        [Description("Mrs")]
        Mrs = 386630001,
        [Description("Miss")]
        Miss = 386630002,
        [Description("Ms")]
        Ms = 386630003,
        [Description("Dr")]
        Dr = 386630004,
        [Description("Other")]
        Other = 386630005
    }
}