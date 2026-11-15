namespace custos.FunctionHelpers.Types
{
    public interface IServiceHealthMetadata
    {
        string Status { get; set; }
        string Version { get; set; }
        DateTime UtcNow { get; set; }
        string Environment { get; set; }
        Dictionary<string, bool> Dependencies { get; set; }
    }
    public class ServiceHealthMetadata : IServiceHealthMetadata
    {
        public string Status { get; set; } = "Healthy";
        public string Version { get; set; } = string.Empty;
        public DateTime UtcNow { get; set; }
        public string Environment { get; set; } = string.Empty;
        public Dictionary<string, bool> Dependencies { get; set; } = new();
    }
}