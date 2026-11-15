namespace Custos.XrmHelpers.Domain.Contracts
{
    public enum InjectionAssetType
    {
        Image = 0,
        Url = 1
    }
    public abstract class InjectionTagBase : IInjectionTag
    {
        public string InjectionToken { get; init; } = string.Empty;
        public object? Value { get; set; }
    }
    public class DataverseInjectionTag : InjectionTagBase
    {
        public string EntityLogicalName { get; init; } = string.Empty;
        public string AttributeLogicalName { get; init; } = string.Empty;
    }

    public class AssetSourceInjectionTag : InjectionTagBase
    {
        public InjectionAssetType Type { get; init; }
        public string ContextKey { get; init; } = string.Empty;
    }

    public class ContextInjectionTag : InjectionTagBase
    {
        public string ContextKey { get; init; } = string.Empty;
    }
}