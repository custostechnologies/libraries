using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Infrastructure.Dataverse
{
    public interface IQuery<TKey>
    {
        ILookupMetadata<TKey> Metadata { get; }
        QueryExpression Build();
        (EntityCollection results, TimeSpan duration) Execute();
    }
}