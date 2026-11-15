namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IDigestMetricProvider<TDto>
    {
        List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records);
    }
}