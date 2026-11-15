using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Domain.Dtos
{
    public static class ConcernInjectionTags
    {
        public static readonly ContextInjectionTag ConcernTypes =
            new()
            {
                InjectionToken = "inConcernTypes",
                ContextKey = nameof(ChildProtectionConcernDto.ConcernTypes)
            };

        public static readonly ContextInjectionTag StudentName =
            new()
            {
                InjectionToken = "inStudentName",
                ContextKey = nameof(ChildProtectionConcernDto.StudentName)
            };

        public static readonly ContextInjectionTag StudentType =
            new()
            {
                InjectionToken = "inStudentType",
                ContextKey = nameof(ChildProtectionConcernDto.StudentType)
            };

        public static readonly ContextInjectionTag SubmittedBy =
            new()
            {
                InjectionToken = "inSubmittedBy",
                ContextKey = nameof(ChildProtectionConcernDto.SubmitterName)
            };

        public static IEnumerable<IInjectionTag> All =>
        [
            ConcernTypes,
            StudentName,
            StudentType,
            SubmittedBy
        ];
    }
}