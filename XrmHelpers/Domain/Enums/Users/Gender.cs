using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum Gender
    {
        [Description("Female")]
        Female = 384810000,
        [Description("Male")]
        Male = 384810001,
        [Description("Non-Binary")]
        NonBinary = 384810002,
        [Description("Not Specified")]
        NotSpecified = 384810003,
        [Description("Other")]
        Other = 384810004,
        [Description("Resolution Error")]
        ResolutionError = 384810005
    }
}