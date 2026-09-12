namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupEngine
    {
        public LookupResult<TKey> Execute<TKey>(TKey luPayload) where TKey : LookupMetadata<TKey>;
    }
}