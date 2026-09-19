using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public interface IQuery<TKey>
    {
        ILookupMetadata<TKey> Metadata { get; }
        QueryExpression Build();
        (EntityCollection results, TimeSpan duration) Execute();
    }
}