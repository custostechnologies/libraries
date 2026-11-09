using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum SubmitterPosition
    {
        [Description("Staff Member")]
        StaffMember = 311940000,
        [Description("Prac Teacher")]
        PracTeacher = 311940001,
        [Description("Board Director")]
        BoardDirector = 311940002,
        [Description("Student")]
        Student = 311940003,
        [Description("Volunteer")]
        Volunteer = 311940004,
        [Description("Visitor")]
        Visitor = 311940005,
        [Description("Contractor")]
        Contractor = 311940006,
        [Description("Other")]
        Other = 311940007
    }
}