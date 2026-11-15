using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class LookupMetadataRequest
    {
        public bool AllowMultipleMatches { get; init; }

        public bool ReturnMultipleReferences { get; init; }

        public required string TargetEntityLogicalName { get; init; }

        public required string TargetAttributeLogicalName { get; init; }

        public required string SourceEntityLogicalName { get; init; }

        public required string SourceAttributeLogicalName { get; init; }

        public required ColumnSet Columns { get; init; }

        public int TopCount { get; init; }
    }
}