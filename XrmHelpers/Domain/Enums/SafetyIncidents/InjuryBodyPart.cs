using System.ComponentModel;

using spgsWebTools.XrmHelpers.Domain.Attributes;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum InjuryBodyPart
    {
        [SeverityFactor(5)]
        [Description("Head")]
        Head = 311940000,
        [Description("Scalp")]
        Scalp = 311940001,
        [Description("Ears")]
        Ears = 311940002,

        [SeverityFactor(2)]
        [Description("Eyes")]
        Eyes = 311940003,
        [Description("Nose")]
        Nose = 311940004,
        [Description("Mouth")]
        Mouth = 311940005,
        [Description("Teeth")]
        Teeth = 311940006,
        [Description("Tongue")]

        [SeverityFactor(4)]
        Tongue = 311940007,
        [Description("Neck")]
        Neck = 311940008,
        [Description("Shoulders")]
        Shoulders = 311940009,
        [Description("Arms")]
        Arms = 311940010,
        [Description("Hands")]
        Hands = 311940011,
        [Description("Fingers")]
        Fingers = 311940012,
        [Description("Chest")]
        Chest = 311940013,
        [Description("Stomach")]
        Stomach = 311940014,
        [Description("Back")]
        Back = 311940015,
        [Description("Hips")]
        Hips = 311940016,
        [Description("Torso")]
        Torso = 311940017,
        [Description("Legs")]
        Legs = 311940018,
        [Description("Knees")]
        Knees = 311940019,
        [Description("Feet")]
        Feet = 311940020,
        [Description("Toes")]
        Toes = 311940021,
        [Description("Ankle")]
        Ankle = 311940022,
        [Description("Other")]
        Other = 311940023
    }
}