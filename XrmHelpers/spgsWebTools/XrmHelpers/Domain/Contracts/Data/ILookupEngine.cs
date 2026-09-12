namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupEngine
    {
        public ILookupResult<TKey> Execute<TKey>(TKey metadata) where TKey : ILookupMetadata<TKey>;
    }
}