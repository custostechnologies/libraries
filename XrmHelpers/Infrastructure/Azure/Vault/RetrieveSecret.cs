/*
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace custos.XrmHelpers.Infrastructure.Azure.Vault
{
    public class RetrieveSecret
    {
        private readonly string VAULT_NAME = "kvcustosdev";
        public async Task<SecretClient> GetSecretClient()
        {
            var kvUri = new Uri("https://" + VAULT_NAME + "vault.azure.net");

            var managedId = new DefaultAzureCredential(
                new DefaultAzureCredentialOptions
                {
                    ManagedIdentityClientId = Environment.GetEnvironmentVariable("MANAGED_IDENTITY_CLIENT_ID"),
                    Diagnostics =
                    {
                        IsLoggingEnabled = true
                    }
                }
            );

            return new SecretClient(
                kvUri,
                managedId
            );
        }
        public async Task<string> RetrieveAzureSecretValue(string secretName, SecretClient sc)
        {
            var secret = await sc.GetSecretAsync(
               secretName
            );

            return secret.Value.Value;
        }
    }
}
*/