using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum CustomIncidentStatus
    {
        [Description("Active")]
        Active = 0,
        [Description("Inactive")]
        Inactive = 1
    }
}