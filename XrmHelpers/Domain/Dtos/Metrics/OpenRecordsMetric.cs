namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class OpenRecordsMetricProvider<TDto> : IDigestMetricProvider<TDto>
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>()
            {
            new DigestMetric
            {
                Label = "Open Records",
                Value = records.Count.ToString()
            }
            };

            return metrics;
        }
    }
}