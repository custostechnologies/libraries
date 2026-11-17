using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public sealed class OverdueTaskMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : TaskDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>()
            {
                new DigestMetric
                {
                    Label = "Overdue Tasks",
                    Value = records
                        .Where(task => task.DueDate <= DateTime.UtcNow)
                        .Count()
                        .ToString()
                }
            };

            return metrics;
        }
    }
    public sealed class TaskTypeMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : TaskDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>();
            var groups = records
                .GroupBy(
                    dto => dto.TaskType ?? TaskType.Other
                )
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
    public sealed class TaskStatusReasonMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : TaskDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>();
            var groups = records
                .GroupBy(dto => dto.StatusReason ?? ActivityStatusReason.InProgress)
                .ToList();

            foreach (var group in groups)
            {
                metrics.Add(
                    new DigestMetric
                    {
                        Label = $"Total for status '{EnumExtensions.GetDescription(group.Key)}'",
                        Value = group.Count().ToString()
                    }
                );
            }

            return metrics;
        }
    }
}