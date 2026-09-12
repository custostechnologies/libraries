using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupMetadata<TKey>
    {
        // what needs to be resolved
        public TKey TargetRecordKey { get; }
        public string TargetKeyType { get; }
        public bool AllowMultipleMatches { get; }
        public bool ReturnMultipleReferences { get; }
        public string TargetEntityLogicalName { get; }
        public string TargetAttributeLogicalName { get; }
        public string SourceEntityLogicalName { get; }
        public string SourceAttributeLogicalName { get; }
    }
    public interface ILookupError
    {
        // why did resolution fail?
        public string Message { get; }
        public LookupFailureReasons Reason { get; }
    }
    public interface ILookupWarning
    {
        // are there any flags to report?
        public string Message { get; }
    }
    public interface ILookupSuccess
    {
        // what reference(s) was/were found
        public EntityReferenceCollection References { get; }
    }
    public interface ILookupResult<TKey>
    {
        public ILookupMetadata<TKey> Metadata { get; }
        public LookupMethod Method { get; }
        public int MatchingRecordCount { get; }
        public bool RetrievedFromCache { get; }
        public TimeSpan LookupDuration { get; }
        public IReadOnlyList<ILookupError> Errors { get; }
        public IReadOnlyList<ILookupWarning> Warnings { get; }
        public IReadOnlyList<ILookupSuccess> Successes { get; }
        public LookupStatus Status { get; }
        public bool Succeeded { get; }
    }
}