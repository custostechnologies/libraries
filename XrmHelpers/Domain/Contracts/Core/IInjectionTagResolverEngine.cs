namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IInjectionTagResolverEngine
    {
        void PopulateDataverseTags<TDto>(
            TDto dto,
            IList<IInjectionTag> tags
        );
    }
}