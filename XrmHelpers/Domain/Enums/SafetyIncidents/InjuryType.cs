using System.ComponentModel;

using Custos.XrmHelpers.Domain.Attributes;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum InjuryType
    {
        [SeverityFactor(2)]
        [Description("Burns")]
        Burns = 311940000,

        [SeverityFactor(5)]
        [Description("Cardiac Arrest")]
        CardiacArrest = 311940001,
        [Description("Cuts Bruises")]
        CutsBruises = 311940002,

        [SeverityFactor(2)]
        [Description("Exposure to a Toxic Substance")]
        ExposureToAToxicSubstance = 311940003,

        [SeverityFactor(2)]
        [Description("Head Face Injury")]
        HeadFaceInjury = 311940004,

        [SeverityFactor(3)]
        [Description("Concussion")]
        Concussion = 311940005,

        [SeverityFactor(4)]
        [Description("Loss of Consciousness")]
        LossOfConsciousness = 311940006,
        [Description("Sprain Fracture")]
        SprainFracture = 311940007,
        [Description("Stress")]
        Stress = 311940008,
        [Description("Aggravation of Injury")]
        AggravationOfInjury = 311940009,

        [SeverityFactor(4)]
        [Description("Amputation")]
        Amputation = 311940010,

        [SeverityFactor(5)]
        [Description("Anaphylaxis")]
        Anaphylaxis = 311940011,
        [Description("Bite Sting")]
        BiteSting = 311940012,
        [Description("Dental")]
        Dental = 311940013,
        [Description("Dizziness")]
        Dizziness = 311940014,

        [SeverityFactor(5)]
        [Description("Electric Shock")]
        ElectricShock = 311940015,
        [Description("Eye Injury")]
        EyeInjury = 311940016,
        [Description("Inflammation")]
        Inflammation = 311940017,
        [Description("Muscle")]
        Muscle = 311940018,
        [Description("Nausea Vomiting")]
        NauseaVomiting = 311940019,
        [Description("Nose Bleed")]
        NoseBleed = 311940020,

        [SeverityFactor(3)]
        [Description("Seizure")]
        Seizure = 311940021,

        [Description("Psychological")]
        Psychological = 311940022,
        [Description("Puncture")]
        Puncture = 311940023,
        [Description("Rash")]
        Rash = 311940024,

        [SeverityFactor(4)]
        [Description("Spinal Injury")]
        SpinalInjury = 311940025,
        [Description("Other")]
        Other = 311940026,
        [Description("N/A")]
        NA = 311940027
    }
}