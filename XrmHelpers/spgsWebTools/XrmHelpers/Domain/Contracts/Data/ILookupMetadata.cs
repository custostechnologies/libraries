using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupMetadata
    {
        object TargetRecordKey { get; }
    }
    public interface ILookupMetadata<TKey> : ILookupMetadata
    {
        // what needs to be resolved
        new TKey TargetRecordKey { get; }
        string TargetKeyType { get; }
        bool AllowMultipleMatches { get; }
        bool ReturnMultipleReferences { get; }
        string TargetEntityLogicalName { get; }
        string TargetAttributeLogicalName { get; }
        string SourceEntityLogicalName { get; }
        string SourceAttributeLogicalName { get; }
        ColumnSet Columns { get; }
        int TopCount { get; }
    }
    public interface ILookupError
    {
        // why did resolution fail?
        string Message { get; }
        LuFailureReason Reason { get; }
    }
    public interface ILookupWarning
    {
        // are there any flags to report?
        string Message { get; }
    }
    public interface ILookupSuccess
    {
        // what reference(s) was/were found
        List<EntityReference> References { get; }
    }
    public interface ILookupResult<TKey>
    {
        ILookupMetadata<TKey> Metadata { get; }
        LuMethod Method { get; }
        int MatchingRecordCount { get; }
        bool RetrievedFromCache { get; }
        TimeSpan LookupDuration { get; }
        IReadOnlyList<ILookupError> Errors { get; }
        IReadOnlyList<ILookupWarning> Warnings { get; }
        IReadOnlyList<ILookupSuccess> Successes { get; }
        LuStatus Status { get; }
        bool Succeeded { get; }
        string GenerateDetailedLogSummary();
        string GenerateLogSummary();
    }
}