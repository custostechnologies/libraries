using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupMetadataFactory
    : ILookupMetadataFactory
    {
        public ILookupMetadata Create(
            Type keyType,
            object keyValue,
            LookupMetadataRequest request)
        {
            if (keyType == typeof(int))
            {
                return new LookupMetadata<int>(
                    (int)keyValue,
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

            if (keyType == typeof(Guid))
            {
                return new LookupMetadata<Guid>(
                    (Guid)keyValue,
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

            return new LookupMetadata<string>(
                keyValue.ToString() ?? string.Empty,
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