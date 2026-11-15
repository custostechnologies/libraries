using custos.XrmHelpers.Domain.Contracts;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace custos.FunctionHelpers.Types
{
    public interface IDataverseService
    {
        Task<ServiceClient> GetServiceClientAsync(IAuthValidationResult validationResult, string secretName);
        Task<bool> ExecuteTest(IAuthValidationResult validationResult);
    }
}