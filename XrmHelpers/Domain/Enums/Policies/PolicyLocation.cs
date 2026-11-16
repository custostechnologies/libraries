using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum PolicyLocation
    {
        [Description("manageCompliance")]
        manageCompliance = 386630000,
        [Description("Ideagen CompliSpace")]
        IdeagenComplispace = 386630001,
        [Description("Unknown")]
        Unknown = 386630002
    }
}