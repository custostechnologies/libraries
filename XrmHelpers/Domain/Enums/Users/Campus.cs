using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum Campus
    {
        [Description("Pre-K")]
        PreK = 384810000,
        [Description("Junior")]
        Junior = 384810001,
        [Description("Secondary")]
        Secondary = 384810002,
        [Description("Across the School")]
        AcrossTheSchool = 384810003,
        [Description("Other or None")]
        OtherOrNone = 384810004,
        [Description("Resolution Error")]
        ResolutionError = 384810005
    }
}