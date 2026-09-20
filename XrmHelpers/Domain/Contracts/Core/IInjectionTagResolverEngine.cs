namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IInjectionTagResolverEngine
    {
        void PopulateDataverseTags<TDto>(
            TDto dto,
            IList<IInjectionTag> tags
        );
        void PopulateDataverseTag<TDto>(
            TDto dto,
            IList<IInjectionTag> tags,
            string token,
            string value
        );
    }
}