using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum IncidentCategory
    {
        [Description("Abduction")]
        Abduction = 311940000,
        [Description("Absconding Missing Student")]
        AbscondingMissingStudent = 311940001,
        [Description("Assault")]
        Assault = 311940002,
        [Description("Bullying and Harassment")]
        BullyingAndHarassment = 311940003,
        [Description("Chemical")]
        Chemical = 311940004,
        [Description("Collision")]
        Collision = 311940005,
        [Description("Dangerous Animal")]
        DangerousAnimal = 311940006,
        [Description("Electrical")]
        Electrical = 311940007,
        [Description("Entrapment")]
        Entrapment = 311940008,
        [Description("Fall From Height")]
        FallFromHeight = 311940009,
        [Description("Foreign Object")]
        ForeignObject = 311940010,
        [Description("Hazardous Dangerous Substance")]
        HazardousDangerousSubstance = 311940011,
        [Description("Health Issue Medical Condition")]
        HealthIssueMedicalCondition = 311940012,
        [Description("Hit Struck By Object")]
        HitStruckByObject = 311940013,
        [Description("Homestay Issue")]
        HomestayIssue = 311940014,
        [Description("Hot Work")]
        HotWork = 311940015,
        [Description("Inappropriate Behaviour")]
        InappropriateBehaviour = 311940016,
        [Description("Manual Handling")]
        ManualHandling = 311940017,
        [Description("Musculoskeletal Injury")]
        MusculoskeletalInjury = 311940018,
        [Description("Noise")]
        Noise = 311940019,
        [Description("Plant Equipment")]
        PlantEquipment = 311940020,
        [Description("Sharps")]
        Sharps = 311940021,
        [Description("Slip Trip Fall")]
        SlipTripFall = 311940022,
        [Description("System of Work Failure")]
        SystemOfWorkFailure = 311940023,

        // Same value as SystemOfWorkFailure in source TS
        [Description("Unclassified")]
        Unclassified = 311940023
    }
}