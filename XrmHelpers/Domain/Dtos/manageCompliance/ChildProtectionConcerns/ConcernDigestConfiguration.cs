using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class ConcernDigestConfiguration : IDigestConfiguration<ChildProtectionConcernDto>
    {
        public string Title => "Child Protection Concern Digest";
        public string Description => "Weekly Concern Summary";
        public IReadOnlyCollection<IDigestMetricProvider<ChildProtectionConcernDto>> Metrics =>
        [
            new OpenRecordsMetricProvider<ChildProtectionConcernDto>(),
            new ConcernStudentTypeMetricProvider<ChildProtectionConcernDto>()
        ];
    }
}