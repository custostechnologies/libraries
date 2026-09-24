namespace spgsWebTools.FunctionHelpers.Types
{
    public class ConfigOptions
    {
        public required string TenantId { get; init; }
        public required string ClientId { get; init; }
        public required string ManagedIdentityClientId { get; init; }
        public required string EnvironmentUri { get; init; }
        public required string VaultName { get; init; }
        public required string AzureEndpointUri { get; init; }
    }
}