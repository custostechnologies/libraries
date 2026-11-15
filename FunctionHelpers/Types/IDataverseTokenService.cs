using Custos.FunctionHelpers.Types;
using Azure.Core;
using Custos.XrmHelpers.Domain.Contracts;
namespace Custos.FunctionHelpers.Types
{
    public interface IDataverseTokenService
    {
        Task<AccessToken> GetTokenAsync(IAuthValidationResult validationResult, string secretName);
    }
}