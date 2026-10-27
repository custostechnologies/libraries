using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class WorkersCompensationClaimDigestConfiguration : IDigestConfiguration<WorkersCompensationClaimDto>
    {
        public string Title => "Safety Incident Digest";
        public string Description => "Weekly Incident Summary";
        public IReadOnlyCollection<IDigestMetricProvider<WorkersCompensationClaimDto>> Metrics =>
        [
            new OpenRecordsMetricProvider<WorkersCompensationClaimDto>()
        ];
    }
}