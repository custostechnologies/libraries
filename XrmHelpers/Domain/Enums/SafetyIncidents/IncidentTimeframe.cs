using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum IncidentTimeframe
    {
        [Description("Before School")]
        BeforeSchool = 311940000,
        [Description("In Class")]
        InClass = 311940001,
        [Description("Between Classes")]
        BetweenClasses = 311940002,
        [Description("Recess")]
        Recess = 311940003,
        [Description("Lunchtime")]
        Lunchtime = 311940004,
        [Description("After School / School Day")]
        AfterSchoolSchoolDay = 311940005,
        [Description("Weekday Inter-School Sport")]
        WeekdayInterSchoolSport = 311940006,
        [Description("Weekend")]
        Weekend = 311940007,
        [Description("Weekend School Sport")]
        WeekendSchoolSport = 311940008,
        [Description("Excursion")]
        Excursion = 311940009,
        [Description("Camp")]
        Camp = 311940010,
        [Description("Public Holiday")]
        PublicHoliday = 311940011,
        [Description("Other")]
        Other = 311940012
    }
}