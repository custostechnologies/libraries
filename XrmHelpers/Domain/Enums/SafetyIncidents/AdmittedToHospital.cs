using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum AdmittedToHospital
    {
        [Description("Yes")]
        Yes = 311940000,
        [Description("No")]
        No = 311940001,
        [Description("Unsure")]
        Unsure = 311940002
    }
}