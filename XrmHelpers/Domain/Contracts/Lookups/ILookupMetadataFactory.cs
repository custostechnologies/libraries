using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface ILookupMetadataFactory
    {
        ILookupMetadata<TKey> Create<TKey>(
            TKey keyValue,
            LookupMetadataRequest request
        );
    }
}