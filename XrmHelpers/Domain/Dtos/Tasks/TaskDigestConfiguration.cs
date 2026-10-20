using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class TaskDigestConfiguration : IDigestConfiguration<TaskDto>
    {
        public string Title => "Task and Activity Digest";
        public string Description => "Weekly Task Summary";
        public IReadOnlyCollection<IDigestMetricProvider<TaskDto>> Metrics =>
        [
            new OpenRecordsMetricProvider<TaskDto>(),
            new OverdueTaskMetricProvider<TaskDto>(),
            new TaskTypeMetricProvider<TaskDto>()
        ];
    }
}