using System.Security.Claims;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class AuthValidationResult : IAuthValidationResult
    {
        public required ClaimsPrincipal ClaimsPrincipal { get; init; }
        public required string ClientId { get; init; }
        public bool IsAppToken { get; init; }
        public bool IsDelegatedToken { get; init; }
        public required string AccessToken { get; init; }
    }
}