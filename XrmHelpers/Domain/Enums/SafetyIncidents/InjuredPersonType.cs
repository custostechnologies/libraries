using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum InjuredPersonType
    {
        [Description("Staff Member")]
        StaffMember = 311940000,
        [Description("Student")]
        Student = 311940001,
        [Description("Volunteer")]
        Volunteer = 311940002,
        [Description("Visitor")]
        Visitor = 311940003,
        [Description("Contractor")]
        Contractor = 311940004
    }
}