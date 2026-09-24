using spgsWebTools.FunctionHelpers.Types;
using Azure.Core;
using spgsWebTools.XrmHelpers.Domain.Contracts;
namespace spgsWebTools.FunctionHelpers.Types
{
    public interface IDataverseTokenService
    {
        Task<AccessToken> GetTokenAsync(IAuthValidationResult validationResult, string secretName);
    }
}