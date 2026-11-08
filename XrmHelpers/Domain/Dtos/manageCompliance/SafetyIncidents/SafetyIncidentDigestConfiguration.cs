using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class SafetyIncidentDigestConfiguration : IDigestConfiguration<SafetyIncidentDto>
    {
        public string Title => "Safety Incident Digest";
        public string Description => "Weekly Incident Summary";
        public IReadOnlyCollection<IDigestMetricProvider<SafetyIncidentDto>> Metrics =>
        [
            new OpenRecordsMetricProvider<SafetyIncidentDto>(),
            new PersonTypeMetricProvider<SafetyIncidentDto>(),
            new IncidentTypeMetricProvider<SafetyIncidentDto>()
        ];
    }
}