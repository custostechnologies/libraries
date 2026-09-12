using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupResolver<TKey>
    {
        bool CanResolve(ILookupMetadata<TKey> metadata);
        ColumnSet Columns { get; }
        ILookupResult<TKey> Resolve(ILookupMetadata<TKey> metadata);
    }
}