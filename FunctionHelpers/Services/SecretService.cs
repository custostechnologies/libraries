using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using spgsWebTools.FunctionHelpers.Types;

namespace spgsWebTools.FunctionHelpers.Services
{
    public class SecretService : ISecretService
    {
        private readonly SecretClient _secretClient;
        private readonly IConfiguration _config;
        private TokenCredential _credential;
        private readonly string APPROVAL_HOOK_SECRET_NAME = "spgswebtools-approval-flow-trigger-endpoint";
        private async Task<string> GetSecretAsync(string secretName)
        {
            Console.WriteLine($"SecretService: retrieving {secretName}");
            var secret = await _secretClient.GetSecretAsync(secretName);
            Console.WriteLine($"SecretService: retrieved {secretName}");
            return secret.Value.Value;
        }
        public SecretService(
            IConfiguration config,
            TokenCredential credential
        )
        {
            _config = config;
            _credential = credential;

            Console.WriteLine("entered SecretService constructor");

            var vaultName = _config["VAULT_NAME"] ?? throw new InvalidOperationException("VAULT_NAME not present in configuration. Unable to instantiate SecretClient object. Exiting.");

            Console.WriteLine($"vault name: {vaultName}");

            var vaultUri = new Uri(
                $"https://{vaultName}.vault.azure.net"
            );

            _secretClient = new SecretClient(
                vaultUri,
                _credential
            );
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