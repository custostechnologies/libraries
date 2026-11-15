using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Domain.Dtos
{
    public static class WorkersCompensationClaimInjectionTags
    {
        public static readonly ContextInjectionTag StaffMemberName =
            new()
            {
                InjectionToken = "inStaffName",
                ContextKey = nameof(WorkersCompensationClaimDto.StaffMemberName)
            };
        public static IEnumerable<IInjectionTag> All =>
        [
            StaffMemberName
        ];
    }
}