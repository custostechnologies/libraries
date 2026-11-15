using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum HTMLSnippetType
    {
        [Description("Content Card")]
        ContentCard = 384810000,
        [Description("Inline Table")]
        InlineTable = 384810001,
        [Description("Single Button")]
        SingleButton = 384810002,
        [Description("Across the School")]
        AcrossTheSchool = 384810003,
        [Description("Other or None")]
        OtherOrNone = 384810004,
        [Description("Resolution Error")]
        ResolutionError = 384810005
    }
}