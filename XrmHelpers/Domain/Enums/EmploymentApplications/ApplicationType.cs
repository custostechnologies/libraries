using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum ApplicationType
    {
        [Description("Support")]
        Support = 386630000,
        [Description("Teaching")]
        Teaching = 386630001,
        [Description("Co-Curricular")]
        CoCurricular = 386630002,
        [Description("Other")]
        Other = 386630003
    }
}