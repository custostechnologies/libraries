using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum PolicyType
    {
        [Description("Policy")]
        Policy = 386630000,
        [Description("Procedure")]
        Procedure = 386630001,
        [Description("Policy and Procedure")]
        PolicyProcedure = 386630002
    }
}