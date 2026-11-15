using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum PAOSSubType
    {
        [Description("Co-Curricular")]
        CoCurricular = 386630000,
        [Description("Non-Co-Curricular")]
        NonCoCurricular = 386630001,
        [Description("Not Applicable")]
        NotApplicable = 386630002
    }
}