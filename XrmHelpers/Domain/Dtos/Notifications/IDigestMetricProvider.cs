namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IDigestMetricProvider<TDto>
    {
        DigestMetric BuildMetric(IReadOnlyCollection<TDto> records);
    }
}