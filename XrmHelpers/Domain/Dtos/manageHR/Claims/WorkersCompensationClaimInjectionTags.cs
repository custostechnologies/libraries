using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Domain.Dtos
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