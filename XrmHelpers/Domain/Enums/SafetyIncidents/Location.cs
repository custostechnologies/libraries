using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum Location
    {
        [Description("Playground")]
        Playground = 311940000,
        [Description("Classroom")]
        Classroom = 311940001,
        [Description("Grounds Carpark")]
        GroundsCarpark = 311940002,
        [Description("Offsite")]
        Offsite = 311940003,
        [Description("Office")]
        Office = 311940004,
        [Description("In Transit")]
        InTransit = 311940005,
        [Description("Other")]
        Other = 311940006
    }
}