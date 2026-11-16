using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum PolicyCategory
    {
        [Description("Curriculum")]
        Curriculum = 386630000,

        [Description("Work, Health and Safety")]
        WorkHealthAndSafety = 386630001,

        [Description("Behaviour and Conduct")]
        BehaviourAndConduct = 386630002,

        [Description("Organisation")]
        Organisation = 386630003,

        [Description("Health and Welfare")]
        HealthAndWelfare = 386630004,

        [Description("Community")]
        Community = 386630005,

        [Description("International Students")]
        InternationalStudents = 386630006,

        [Description("Information Technology")]
        InformationTechnology = 386630007,

        [Description("Emergency")]
        Emergency = 386630008,

        [Description("Business and Finance")]
        BusinessAndFinance = 386630009
    }
}