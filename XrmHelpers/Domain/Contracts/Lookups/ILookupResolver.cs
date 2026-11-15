using Microsoft.Xrm.Sdk.Query;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface ILookupResolver<TKey>
    {
        //bool CanResolve(ILookupMetadata<TKey> metadata);
        ILookupResult<TKey> Resolve(ILookupMetadata<TKey> metadata);
    }
}