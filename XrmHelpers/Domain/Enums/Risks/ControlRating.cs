using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum ControlRating
    {
        [Description("Unsatisfactory")]
        Unsatisfactory = 386630000,

        [Description("Poor")]
        Poor = 386630001,

        [Description("Satisfactory")]
        Satisfactory = 386630002,

        [Description("Good")]
        Good = 386630003,

        [Description("Excellent")]
        Excellent = 386630004
    }
}
