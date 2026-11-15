using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Domain.Contracts
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