using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace spgsWebTools.XrmHelpers.Infrastructure.Azure.Vault
{
    public interface ISecretService
    {
        Task<string> GetClientSecretAsync();
        Task<string> GetFlowEndpointAsync();
    }
    public class SecretService : ISecretService
    {
        private readonly SecretClient _secretClient;
        private readonly IConfiguration _config;
        private TokenCredential _credential;
        private readonly string HOOK_SECRET_NAME = "spgswebtools-forcesyncuser-flow-trigger-endpoint";
        private readonly string CLIENT_SECRET_NAME = "spgswebtools-powerplatform-serviceprincipal-client-secret";
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

            var vaultName = _config["VAULT_NAME"]
                ?? throw new InvalidOperationException("VaultName not present in configuration. Unable to instantiate SecretClient object. Exiting.");

            Console.WriteLine($"vault name: {vaultName}");

            var vaultUri = new Uri(
                $"https://{vaultName}.vault.azure.net"
            );

            _secretClient = new SecretClient(
                vaultUri,
                _credential
            );
        }
        public async Task<string> GetClientSecretAsync()
        {
            var secret = await GetSecretAsync(CLIENT_SECRET_NAME);
            return secret;
        }
        public async Task<string> GetFlowEndpointAsync()
        {
            var secret = await GetSecretAsync(HOOK_SECRET_NAME);
            return secret;
        }
    }
}