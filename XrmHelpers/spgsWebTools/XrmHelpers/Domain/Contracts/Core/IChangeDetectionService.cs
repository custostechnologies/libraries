namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IChangeDetectionService<TDto>
    {
        DeltaComparison<TDto> Compare(TDto prev, TDto next);
    }
}