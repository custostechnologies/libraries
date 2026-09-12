using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupMetadataFactory
    {
        ILookupMetadata Create(
            Type keyType,
            object keyValue,
            LookupMetadataRequest request
        );
    }
}