using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupMetadataRequest
    {
        public bool AllowMultipleMatches { get; init; }

        public bool ReturnMultipleReferences { get; init; }

        public string TargetEntityLogicalName { get; init; }

        public string TargetAttributeLogicalName { get; init; }

        public string SourceEntityLogicalName { get; init; }

        public string SourceAttributeLogicalName { get; init; }

        public ColumnSet Columns { get; init; }

        public int TopCount { get; init; }
    }
}