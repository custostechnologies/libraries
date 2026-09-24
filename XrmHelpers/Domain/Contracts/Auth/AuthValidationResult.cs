using System.Security.Claims;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class AuthValidationResult : IAuthValidationResult
    {
        public ClaimsPrincipal? ClaimsPrincipal { get; init; }
        public string? ClientId { get; init; }
        public bool IsAppToken { get; init; }
        public bool IsDelegatedToken { get; init; }
        public string? AccessToken { get; init; }
    }
}