namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IDigestConfiguration<TDto>
    {
        string Title { get; }
        string Description { get; }
        IReadOnlyCollection<IDigestMetricProvider<TDto>> Metrics { get; }
    }
}