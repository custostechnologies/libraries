using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Domain.Dtos
{
    public static class TaskInjectionTags
    {
        public static readonly ContextInjectionTag Type =
            new()
            {
                InjectionToken = "inTaskType",
                ContextKey = nameof(TaskDto.TaskType)
            };

        public static readonly ContextInjectionTag Category =
            new()
            {
                InjectionToken = "inTaskCategory",
                ContextKey = nameof(TaskDto.TaskCategory)
            };

        public static readonly ContextInjectionTag Subject =
            new()
            {
                InjectionToken = "inTaskSubject",
                ContextKey = nameof(TaskDto.Subject)
            };

        public static readonly ContextInjectionTag RegardingRecordName =
            new()
            {
                InjectionToken = "inRegardingRecordName",
                ContextKey = nameof(TaskDto.RegardingEntityLogicalName)
            };

        public static IEnumerable<IInjectionTag> All =>
        [
            Type,
            Category,
            Subject,
            RegardingRecordName
        ];
    }
}