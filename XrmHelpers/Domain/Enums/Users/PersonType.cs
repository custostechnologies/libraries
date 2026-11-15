using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum PersonType
    {
        [Description("Staff Member")]
        StaffMember = 311940000,
        [Description("Contractor")]
        Contractor = 311940001,
        [Description("Visitor")]
        Visitor = 311940002,
        [Description("Student")]
        Student = 311940003,
        [Description("Other")]
        Other = 311940004
    }
}