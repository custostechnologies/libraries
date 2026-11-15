using custos.XrmHelpers.Domain.Dtos;
using custos.XrmHelpers.Domain.Enums;

namespace custos.XrmHelpers.Domain.Contracts
{
    public sealed class ConcernStudentTypeMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto: ChildProtectionConcernDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            var grouped = records
                .GroupBy(
                    record =>
                    {
                        if (record.StudentType != null)
                        {
                            return ((StudentType)record.StudentType).ToString();
                        } else
                        {
                            return "Unknown";
                        }
                    }
                );

            List<DigestMetric> metrics = new ();

            foreach (var group in grouped)
            {
                metrics.Add(new DigestMetric
                {
                    Label = $"Total for `{group.Key}`",
                    Value = group.Count().ToString()
                });
            }

            return metrics;
        }
    }
}