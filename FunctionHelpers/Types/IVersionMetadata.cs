namespace spgsWebTools.FunctionHelpers.Types
{
    interface IVersionMetadata
    {
        string Application { get; set; }
        string Environment { get; set; }
        string Version { get; set; }
        string InformationalVersion { get; set; }
        string Runtime { get; set; }
        string InstanceId { get; set; }
        string DeploymentSlot { get; set; }
        string MachineName { get; set; }
        DateTime UtcNow { get; set; }
    }
}