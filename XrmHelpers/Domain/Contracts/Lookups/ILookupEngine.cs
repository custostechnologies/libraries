namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface ILookupEngine
    {
        public ILookupResult<TKey> Execute<TKey>(ILookupMetadata<TKey> metadata);
    }
}