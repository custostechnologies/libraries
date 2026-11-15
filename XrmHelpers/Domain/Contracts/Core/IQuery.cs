using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Infrastructure.Dataverse
{
    public interface IQuery<TKey>
    {
        ILookupMetadata<TKey> Metadata { get; }
        QueryExpression Build();
        (EntityCollection results, TimeSpan duration) Execute();
    }
}