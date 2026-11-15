using Azure.Core;
using Microsoft.Extensions.Logging;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using custos.FunctionHelpers.Types;
using System.Diagnostics;

namespace custos.FunctionHelpers.Services
{
    public class SecretService : ISecretService
    {
        private readonly ILogger<SecretService> _logger;
        private readonly SecretClient _secretClient;
        private readonly IConfiguration _config;
        private TokenCredential _credential;
        public SecretService(
            ILogger<SecretService> logger,
            IConfiguration config,
            TokenCredential credential
        )
        {
            _logger = logger;
            _config = config;
            _credential = credential;

            _logger.LogInformation("entered SecretService constructor");

            var vaultName = _config["VAULT_NAME"] ?? throw new InvalidOperationException("VAULT_NAME not present in configuration. Unable to instantiate SecretClient object. Exiting.");

            _logger.LogInformation($"vault name: {vaultName}");

            var vaultUri = new Uri(
                $"https://{vaultName}.vault.azure.net"
            );

            _secretClient = new SecretClient(
                vaultUri,
                _credential
            );
        }
        private async Task<string> GetSecretAsync(string secretName)
        {
            var start = Stopwatch.StartNew();
            _logger.LogInformation($"SecretService: retrieving {secretName}");

            var secret = await _secretClient.GetSecretAsync(secretName);

            _logger.LogInformation($"SecretService: retrieved {secretName} in {start.ElapsedMilliseconds}ms.");
            return secret.Value.Value;
        }
        public async Task<string> GetClientSecretAsync(string secretName)
        {
            var secret = await GetSecretAsync(secretName);
            return secret;
        }
        public async Task<string> GetFlowEndpointAsync(string secretName)
        {
            var secret = await GetSecretAsync(secretName);
            return secret;
        }
    }
}