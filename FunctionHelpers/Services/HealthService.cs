using Microsoft.Azure.Functions.Worker.Http;
using Custos.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Logging;
using Custos.FunctionHelpers.Utilities;
using Custos.FunctionHelpers.Logging;
using Custos.FunctionHelpers.Types;
using Microsoft.Extensions.Configuration;
using Azure;
using Azure.Core;
using Azure.Security.KeyVault.Secrets;
using System.Reflection;

namespace Custos.FunctionHelpers.Services
{
    public class HealthService
    {
        private readonly AzureFxCustomLogger<HealthService> _logger;
        private readonly IConfiguration _config;
        private readonly TokenCredential _credential;
        private readonly IAuthorisationService _authService;
        private readonly IDataverseService _dvService;
        public HealthService(
            AzureFxCustomLogger<HealthService> logger,
            IAuthorisationService authService,
            IDataverseService dvService,
            IConfiguration config,
            TokenCredential credential
        )
        {
            _logger = logger;
            _authService = authService;
            _dvService = dvService;
            _config = config;
            _credential = credential;
        }
        public async Task<(bool health, IAuthValidationResult? validationResult)> TestAuthorisationService(HttpRequestData req)
        {
            try
            {
                /* RETRIEVE USER TOKEN */
                var token = AuthorisationHeader.Parse(req);
                if (string.IsNullOrWhiteSpace(token))
                {
                    _logger.LogError("Request token is empty.");
                    return (false, null);
                }

                /* TEST AUTHORISATION SERVICE */
                var validationResult = await _authService.ValidateIncomingRequestAsync(token);
                _logger.LogInformation("Validation complete and request has been authorised to continue. Parsing id parameter and calling retrieval service...");

                if (validationResult != null) return (true, validationResult);

                return (false, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encountered when testing AuthService health");
                return (false, null);
            }
        }
        public async Task<bool> TestDataverseService(IAuthValidationResult validationResult)
        {
            try
            {
                return await _dvService.ExecuteTest(validationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error encountered when testing Dataverse service health.");
                return false;
            }
        }
        public async Task<bool> TestKeyVault()
        {
            try
            {
                var vaultName = _config["VAULT_NAME"];

                if (string.IsNullOrEmpty(vaultName))
                {
                    _logger.LogInformation("vaultName config is empty.");
                    return false;
                }
                var client = new SecretClient(
                    new Uri($"https://{vaultName}.vault.azure.net"),
                    _credential);
                Response<KeyVaultSecret> secret = await client.GetSecretAsync("health-check");
                
                _logger.LogInformation("Successfully connected to Key Vault and retrieved health-check secret.");

                return !string.IsNullOrWhiteSpace(secret.Value.Value);

            } catch (RequestFailedException ex) when (ex.Status == 404)
            {
                _logger.LogError(ex, "health-check secret not found.");
                return false;
            } catch (RequestFailedException ex) when (ex.Status == 403)
            {
                _logger.LogError(ex, "Access denied to Key Vault.");
                return false;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error encountered when testing Key Vault.");
                return false;
            }
        }
        public async Task<IServiceHealthMetadata> EvaluateServiceHealth(HttpRequestData req)
        {
            var authResult = await TestAuthorisationService(req);
            bool authHealthy = authResult.health;

            bool dataverseHealthy = false;
            if (authResult.validationResult != null)
            {
                dataverseHealthy = await TestDataverseService(authResult.validationResult);
            }

            bool keyVaultHealthy = await TestKeyVault();
            bool overallHealthy = authHealthy && dataverseHealthy && keyVaultHealthy;

            return new ServiceHealthMetadata
            {
                Status = overallHealthy
                    ? "Healthy"
                    : "Unhealthy",
                Version = Assembly
                    .GetExecutingAssembly()
                    .GetName()
                    .Version?
                    .ToString() ?? "UNKNOWN",
                UtcNow = DateTime.UtcNow,
                Environment =
                    Environment.GetEnvironmentVariable(
                    "AZURE_FUNCTIONS_ENVIRONMENT")
                    ?? "UNKNOWN",
                Dependencies = new Dictionary<string, bool>
                {
                    ["AuthorisationService"] = authHealthy,
                    ["Dataverse"] = dataverseHealthy,
                    ["KeyVault"] = keyVaultHealthy
                }
            };
        }
    }
}