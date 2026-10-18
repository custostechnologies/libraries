namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class OpenRecordsMetricProvider<TDto> : IDigestMetricProvider<TDto>
    {
        public DigestMetric BuildMetric(IReadOnlyCollection<TDto> records)
        {
            return new DigestMetric
            {
                Label = "Open Records",
                Value = records.Count.ToString()
            };
        }
    }
}