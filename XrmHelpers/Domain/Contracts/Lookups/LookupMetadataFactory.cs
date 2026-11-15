using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class LookupMetadataFactory
    : ILookupMetadataFactory
    {
        public ILookupMetadata<TKey> Create<TKey>(
            TKey keyValue,
            LookupMetadataRequest request)
        {
            return new LookupMetadata<TKey>(
                keyValue,
                request.AllowMultipleMatches,
                request.ReturnMultipleReferences,
                request.TargetEntityLogicalName,
                request.TargetAttributeLogicalName,
                request.SourceEntityLogicalName,
                request.SourceAttributeLogicalName,
                request.Columns,
                request.TopCount
            );
        }
    }
}