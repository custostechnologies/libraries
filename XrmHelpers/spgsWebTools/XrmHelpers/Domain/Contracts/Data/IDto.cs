namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IDto
    {
        static string SourceEntityLogicalName { get; }
        static string TargetEntityLogicalName { get; }
    }
}