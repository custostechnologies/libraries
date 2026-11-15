using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum ComplianceFlag
    {
        [Description("Registration Guidelines")]
        RegistrationGuidelines = 386630000,

        [Description("Overseas Students")]
        OverseasStudents = 386630001,

        [Description("Child Protection")]
        ChildProtection = 386630002,

        [Description("National Quality Standards")]
        NationalQualityStandards = 386630003,

        [Description("Workplace Safety")]
        WorkplaceSafety = 386630004
    }
}