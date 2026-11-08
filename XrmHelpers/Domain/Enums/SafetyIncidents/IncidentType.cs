using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum IncidentType
    {
        [Description("Resulting in Injury")]
        ResultingInInjury = 311940000,

        [Description("Near Miss")]
        NearMiss = 311940001
    }
}