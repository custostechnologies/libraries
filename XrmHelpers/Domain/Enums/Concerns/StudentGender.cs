using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum StudentGender
    {
        [Description("Male")]
        Male = 311940000,

        [Description("Female")]
        Female = 311940001,

        [Description("Non-Binary")]
        NonBinary = 311940002,

        [Description("Other")]
        Other = 311940003
    }
}