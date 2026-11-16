using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum RiskFlag
    {
        [Description("Registration Guidelines")]
        RegistrationGuidelines = 386630000,

        [Description("WHS")]
        Whs = 386630001,

        [Description("Student Duty of Care")]
        StudentDutyOfCare = 386630002,

        [Description("Child Protection")]
        ChildProtection = 386630003,

        [Description("BCM")]
        Bcm = 386630004,

        [Description("Fraud and Corruption")]
        FraudAndCorruption = 386630005,

        [Description("Reputation")]
        Reputation = 386630006
    }
}