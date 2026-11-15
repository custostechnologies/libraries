using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum NESAAccreditation
    {
        [Description("None")]
        None = 384810000,
        [Description("Conditional")]
        Conditional = 384810001,
        [Description("Provisional")]
        Provisional = 384810002,
        [Description("Proficient")]
        Proficient = 384810003,
        [Description("Highly Accomplished")]
        HighlyAccomplished = 384810004,
        [Description("Lead Teacher")]
        LeadTeacher = 384810005
    }
}