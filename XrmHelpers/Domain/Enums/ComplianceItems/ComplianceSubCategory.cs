using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum ComplianceSubCategory
    {
        [Description("Education Act")]
        EducationAct = 386630000,

        [Description("Overseas Students")]
        OverseasStudents = 386630001,

        [Description("Child Protection")]
        ChildProtection = 386630002,

        [Description("Work, Health and Safety")]
        WHS = 386630003
    }
}