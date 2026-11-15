using System.ComponentModel;

using custos.XrmHelpers.Domain.Attributes;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum IncidentCategory
    {
        [SeverityFactor(5)]
        [Description("Abduction")]
        Abduction = 311940000,
        [SeverityFactor(4)]
        [Description("Absconding Missing Student")]
        AbscondingMissingStudent = 311940001,
        [SeverityFactor(4)]
        [Description("Assault")]
        Assault = 311940002,
        [SeverityFactor(3)]
        [Description("Bullying and Harassment")]
        BullyingAndHarassment = 311940003,
        [SeverityFactor(4)]
        [Description("Chemical")]
        Chemical = 311940004,
        [SeverityFactor(4)]
        [Description("Collision")]
        Collision = 311940005,
        [SeverityFactor(4)]
        [Description("Dangerous Animal")]
        DangerousAnimal = 311940006,
        [SeverityFactor(4)]
        [Description("Electrical")]
        Electrical = 311940007,
        [SeverityFactor(4)]
        [Description("Entrapment")]
        Entrapment = 311940008,
        [SeverityFactor(4)]
        [Description("Fall From Height")]
        FallFromHeight = 311940009,
        [SeverityFactor(2)]
        [Description("Foreign Object")]
        ForeignObject = 311940010,
        [SeverityFactor(3)]
        [Description("Hazardous Dangerous Substance")]
        HazardousDangerousSubstance = 311940011,
        [SeverityFactor(3)]
        [Description("Health Issue Medical Condition")]
        HealthIssueMedicalCondition = 311940012,
        [SeverityFactor(3)]
        [Description("Hit Struck By Object")]
        HitStruckByObject = 311940013,
        [SeverityFactor(4)]
        [Description("Homestay Issue")]
        HomestayIssue = 311940014,
        [SeverityFactor(4)]
        [Description("Hot Work")]
        HotWork = 311940015,
        [SeverityFactor(2)]
        [Description("Inappropriate Behaviour")]
        InappropriateBehaviour = 311940016,
        [SeverityFactor(2)]
        [Description("Manual Handling")]
        ManualHandling = 311940017,
        [SeverityFactor(3)]
        [Description("Musculoskeletal Injury")]
        MusculoskeletalInjury = 311940018,
        [SeverityFactor(1)]
        [Description("Noise")]
        Noise = 311940019,
        [SeverityFactor(3)]
        [Description("Plant Equipment")]
        PlantEquipment = 311940020,
        [SeverityFactor(3)]
        [Description("Sharps")]
        Sharps = 311940021,
        [SeverityFactor(3)]
        [Description("Slip Trip Fall")]
        SlipTripFall = 311940022,
        [SeverityFactor(2)]
        [Description("System of Work Failure")]
        SystemOfWorkFailure = 311940023,

        // Same value as SystemOfWorkFailure in source TS
        [SeverityFactor(1)]
        [Description("Unclassified")]
        Unclassified = 311940023
    }
}