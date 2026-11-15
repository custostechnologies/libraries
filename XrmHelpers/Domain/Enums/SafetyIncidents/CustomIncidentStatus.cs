using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum CustomIncidentStatus
    {
        [Description("Active")]
        Active = 0,
        [Description("Inactive")]
        Inactive = 1
    }
}