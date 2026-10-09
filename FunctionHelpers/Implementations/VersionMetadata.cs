using spgsWebTools.FunctionHelpers.Types;

namespace spgsWebTools.FunctionHelpers.Implementations
{
    public class VersionMetadata : IVersionMetadata
    {
        public string Application { get; set; } = string.Empty;
        public string Environment { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string InformationalVersion { get; set; } = string.Empty;
        public string Runtime { get; set; } = string.Empty;
        public string InstanceId { get; set; } = string.Empty;
        public string DeploymentSlot { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;
        public DateTime UtcNow { get; set; }
    }
}