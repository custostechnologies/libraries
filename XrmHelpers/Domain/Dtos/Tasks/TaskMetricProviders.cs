using custos.XrmHelpers.Domain.Dtos;
using custos.XrmHelpers.Domain.Enums;

namespace custos.XrmHelpers.Domain.Contracts
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
                    dto =>
                    {
                        if (dto.TaskType != null)
                        {
                            return ((TaskType)dto.TaskType).ToString();
                        } else
                        {
                            return TaskType.Other.ToString();
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
    public sealed class TaskStatusReasonMetricProvider<TDto> : IDigestMetricProvider<TDto> where TDto : TaskDto
    {
        public List<DigestMetric> BuildMetric(IReadOnlyCollection<TDto> records)
        {
            List<DigestMetric> metrics = new List<DigestMetric>();
            var groups = records
                .GroupBy(
                    dto =>
                    {
                        if (dto.StatusReason != null)
                        {
                            return ((ActivityStatusReason)dto.StatusReason).ToString();
                        } else
                        {
                            return Status.Active.ToString();
                        }
                    }
                )
                .ToList();

            foreach (var group in groups)
            {
                metrics.Add(
                    new DigestMetric
                    {
                        Label = $"Total for status '{group.Key}'",
                        Value = group.Count().ToString()
                    }
                );
            }

            return metrics;
        }
    }
}