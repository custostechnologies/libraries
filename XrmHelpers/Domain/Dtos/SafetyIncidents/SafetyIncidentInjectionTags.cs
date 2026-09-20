using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    public static class SafetyIncidentInjectionTags
    {
        public static readonly ContextInjectionTag IncidentType =
            new()
            {
                InjectionToken = "inIncidentType",
                ContextKey = nameof(SafetyIncidentDto.IncidentType)
            };

        public static readonly ContextInjectionTag IncidentDate =
            new()
            {
                InjectionToken = "inIncidentDate",
                ContextKey = nameof(SafetyIncidentDto.DateTimeOfIncident)
            };

        public static readonly ContextInjectionTag IncidentLocation =
            new()
            {
                InjectionToken = "inIncidentLocation",
                ContextKey = nameof(SafetyIncidentDto.Location)
            };

        public static readonly ContextInjectionTag PersonInvolved =
            new()
            {
                InjectionToken = "inPersonInvolved",
                ContextKey = nameof(SafetyIncidentDto.NameOfInjuredPerson)
            };

        public static IEnumerable<IInjectionTag> All =>
        [
            IncidentType,
            IncidentDate,
            IncidentLocation,
            PersonInvolved
        ];
    }
}