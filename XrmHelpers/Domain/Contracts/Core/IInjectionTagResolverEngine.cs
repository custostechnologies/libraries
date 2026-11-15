namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IInjectionTagResolverEngine
    {
        void PopulateDataverseTags<TDto>(
            TDto dto,
            IList<IInjectionTag> tags
        );
        void PopulateTag<TDto>(
            TDto dto,
            IList<IInjectionTag> tags,
            string token,
            string value
        );
    }
}