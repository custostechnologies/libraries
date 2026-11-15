using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum FormType
    {
        [Description("Casual")]
        Casual = 386630000,
        [Description("Salaried")]
        Salaried = 386630001,
        [Description("Not Selected")]
        NotSelected = 386630002
    }
}