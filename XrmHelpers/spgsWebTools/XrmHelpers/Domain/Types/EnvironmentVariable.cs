namespace spgsWebTools.XrmHelpers.Domain.Types
{
    public class EnvironmentVariable<T>
    {
        public string? SchemaName { get; set; }
        public T? Value { get; set; } 
    }
}