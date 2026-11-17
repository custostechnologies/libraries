using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public sealed class IncidentTypeMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : SafetyIncidentDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>();
            var groups = records
                .GroupBy(dto => dto.IncidentType)
                .ToList();

            foreach (var group in groups)
            {
                metrics.Add(
                    new DigestMetric
                    {
                        Label = $"Total for '{EnumExtensions.GetDescription(group.Key)}'",
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
                .GroupBy(dto => dto.WhoWasInjured)
                .ToList();

            foreach (var group in groups)
            {
                metrics.Add(
                    new DigestMetric
                    {
                        Label = $"Total for '{EnumExtensions.GetDescription(group.Key)}'",
                        Value = group.Count().ToString()
                    }
                );
            }

            return metrics;
        }
    }
}