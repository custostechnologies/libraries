using System.Reflection;
using spgsWebTools.FunctionHelpers.Implementations;

namespace spgsWebTools.FunctionHelpers.Services
{
    public class VersionService
    {
        public async Task<VersionMetadata> ReturnVersionMetadata()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var version =
                assembly.GetName().Version?.ToString()
                ?? "UNKNOWN";

            var informationalVersion =
                assembly
                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    ?.InformationalVersion
                ?? version;

            return new VersionMetadata
            {
                Application =
                        assembly.GetName().Name
                        ?? "UNKNOWN",

                Environment =
                        Environment.GetEnvironmentVariable(
                            "AZURE_FUNCTIONS_ENVIRONMENT")
                        ?? "UNKNOWN",

                Version = version,

                InformationalVersion =
                        informationalVersion,

                Runtime =
                        Environment.Version.ToString(),

                InstanceId =
                        Environment.GetEnvironmentVariable(
                            "WEBSITE_INSTANCE_ID")
                        ?? "LOCAL",

                DeploymentSlot =
                        Environment.GetEnvironmentVariable(
                            "WEBSITE_SLOT_NAME")
                        ?? "LOCAL",

                MachineName =
                        Environment.MachineName,

                UtcNow =
                        DateTime.UtcNow
            };
        }
    }
}