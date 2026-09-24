using Microsoft.Identity.Client;
using Azure.Identity;
using Azure.Core;
using Microsoft.Extensions.Logging;
using spgsWebTools.FunctionHelpers.Types;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.FunctionHelpers.Services
{
    public class DataverseTokenService : IDataverseTokenService
    {
        private string? _cachedClientSecret;
        private readonly ISecretService _secretService;
        private readonly ILogger<DataverseTokenService> _logger;
        private readonly ConfigOptions _configOptions;
        private IConfidentialClientApplication? _confidentialClientApp;
        public DataverseTokenService(
            ILogger<DataverseTokenService> logger,
            ISecretService secretService,
            ConfigOptions configOptions
        )
        {
            _logger = logger;
            _secretService = secretService;
            _configOptions = configOptions;
        }
        public async Task<AccessToken> GetTokenAsync(IAuthValidationResult validationResult, string secretName)
        {
            _logger.LogInformation("Creating ConfidentialClientApp via lazy loading");
            if (_confidentialClientApp == null)
            {
                _cachedClientSecret ??= await _secretService.GetClientSecretAsync(secretName);

                _confidentialClientApp = ConfidentialClientApplicationBuilder
                .Create(_configOptions.ClientId)
                .WithClientSecret(_cachedClientSecret)
                .WithTenantId(_configOptions.TenantId)
                .Build();
            }

            _logger.LogInformation("Entered ServiceClient GetServiceClientAsync(). Using IAuthValidationResult to determine which auth flow to use: OBO or ManagedId");

            if (validationResult.IsAppToken)
            {
                _logger.LogInformation("Attempting to create ServiceClient using SYSTEM-Assigned Managed Identity.");

                var credential = new ManagedIdentityCredential(ManagedIdentityId.SystemAssigned);

                var token = await credential.GetTokenAsync(
                    new TokenRequestContext(
                        new[] { $"{_configOptions.EnvironmentUri}/.default" }
                    )
                );

                _logger.LogInformation($"Acquired Managed Identity token from {_configOptions.EnvironmentUri}/.default endpoint. Returning token to caller.");

                return token;
            }
            else
            {
                _logger.LogInformation("Reading JWT Token from IAuthValidationResult.ClaimsPrincipal");

                var assertion = new UserAssertion(validationResult.AccessToken);

                try
                {
                    _logger.LogInformation("Executing confidentialClientApp.AcquireTokenOnBehalfOf");
                    var authResult = await _confidentialClientApp.AcquireTokenOnBehalfOf(
                        new[]
                        {
                            $"{_configOptions.EnvironmentUri}/user_impersonation"
                        },
                        assertion
                    ).ExecuteAsync();

                    _logger.LogInformation("Returning AccessToken built from authResult to caller.");

                    return new AccessToken(
                        authResult.AccessToken,
                        authResult.ExpiresOn
                    );
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
}