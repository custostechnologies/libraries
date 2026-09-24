using spgsWebTools.XrmHelpers.Domain.Contracts;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace spgsWebTools.FunctionHelpers.Types
{
    public interface IDataverseService
    {
        Task<ServiceClient> GetServiceClientAsync(IAuthValidationResult validationResult, string secretName);
    }
}