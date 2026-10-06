using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Extensions.Logging;
using spgsWebTools.FunctionHelpers.Types;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.FunctionHelpers.Services
{
    public class DataverseService : IDataverseService
    {
        /*
            // security
            // authorisation via user token authentication
            // infrastructure secrets via managed identity authentication
        */
        private readonly ILogger<DataverseService> _logger;
        private readonly ConfigOptions _configOptions;
        private readonly IDataverseTokenService _tokenService;
        public DataverseService(
            ILogger<DataverseService> logger,
            ConfigOptions configOptions,
            IDataverseTokenService tokenService
        )
        {
            _logger = logger;
            _configOptions = configOptions;
            _tokenService = tokenService;
        }
        public async Task<ServiceClient> GetServiceClientAsync(IAuthValidationResult validationResult, string secretName)
        {
            _logger.LogInformation("Attempting to create ServiceClient using user token.");

            try
            {
                var token = await _tokenService.GetTokenAsync(validationResult, secretName);
                var client = new ServiceClient(
                    tokenProviderFunction: uri => Task.FromResult(token.Token),
                    instanceUrl: new Uri(_configOptions.EnvironmentUri)
                );

                _logger.LogInformation("Returning ServiceClient using user authentication.");
                
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unable to retrieve Dataverse token using OBO authentication flow."
                );
                throw;
            }
        }
    }
}