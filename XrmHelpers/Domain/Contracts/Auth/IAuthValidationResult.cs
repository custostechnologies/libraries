using System.Security.Claims;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IAuthValidationResult
    {
        ClaimsPrincipal? ClaimsPrincipal { get; init; }
        string? ClientId { get; init; }
        bool IsAppToken { get; init; }
        bool IsDelegatedToken { get; init; }
        string? AccessToken { get; init; }
    }
}