using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class IncidentTypeMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : SafetyIncidentDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>();
            var groups = records
                .GroupBy(
                    dto =>
                    {
                        if (dto.IncidentType != null)
                        {
                            return ((IncidentType)dto.IncidentType).ToString();
                        } else
                        {
                            return "Unknown";
                        }
                    }
                )
                .ToList();

            foreach (var group in groups)
            {
                metrics.Add(
                    new DigestMetric
                    {
                        Label = $"Total for '{group.Key}'",
                        Value = group.Count().ToString()
                    }
                );
            }

            return metrics;
        }
    }
    public sealed class PersonTypeMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : SafetyIncidentDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>();
            var groups = records
                .GroupBy(
                    dto => ((IncidentType)dto.WhoWasInjured).ToString()
                )
                .ToList();

            foreach (var group in groups)
            {
                metrics.Add(
                    new DigestMetric
                    {
                        Label = $"Total for '{group.Key}'",
                        Value = group.Count().ToString()
                    }
                );
            }

            return metrics;
        }
    }
}