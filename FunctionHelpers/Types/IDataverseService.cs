using Custos.XrmHelpers.Domain.Contracts;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace Custos.FunctionHelpers.Types
{
    public interface IDataverseService
    {
        Task<ServiceClient> GetServiceClientAsync(IAuthValidationResult validationResult, string secretName);
        Task<bool> ExecuteTest(IAuthValidationResult validationResult);
    }
}