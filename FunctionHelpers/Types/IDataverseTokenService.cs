using custos.FunctionHelpers.Types;
using Azure.Core;
using custos.XrmHelpers.Domain.Contracts;
namespace custos.FunctionHelpers.Types
{
    public interface IDataverseTokenService
    {
        Task<AccessToken> GetTokenAsync(IAuthValidationResult validationResult, string secretName);
    }
}