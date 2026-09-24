namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IInjectionTag
    {
        string InjectionToken { get; }
        object? Value { get; set; }
    };
}