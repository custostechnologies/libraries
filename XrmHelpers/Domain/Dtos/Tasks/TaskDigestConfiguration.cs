using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Domain.Contracts
{
    public sealed class TaskDigestConfiguration : IDigestConfiguration<TaskDto>
    {
        public string Title => "Task and Activity Digest";
        public string Description => "Weekly Task Summary";
        public IReadOnlyCollection<IDigestMetricProvider<TaskDto>> Metrics =>
        [
            new OpenRecordsMetricProvider<TaskDto>(),
            new OverdueTaskMetricProvider<TaskDto>(),
            new TaskTypeMetricProvider<TaskDto>(),
            new TaskStatusReasonMetricProvider<TaskDto>()
        ];
    }
}