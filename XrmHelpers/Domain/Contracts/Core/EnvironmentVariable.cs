namespace Custos.XrmHelpers.Domain.Contracts
{
    public class EnvironmentVariable<T>
    {
        public string? SchemaName { get; set; }
        public T? Value { get; set; } 
    }
}