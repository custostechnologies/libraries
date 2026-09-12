namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupError : ILookupError
    {
        public LookupError(string message, LookupFailureReasons reason)
        {
            Message = message;
            Reason = reason;
        }
        public string Message { get; }
        public LookupFailureReasons Reason { get; }
    }
    public class LookupWarning : ILookupWarning
    {
        public LookupWarning(string message)
        {
            Message = message;
        }
        public string Message { get; }
    }
    public class LookupSuccess : ILookupSuccess
    {
        public LookupSuccess(EntityReferenceCollection refs)
        {
            References = refs;
        }
        public EntityReferenceCollection References { get; }
    }
    public class LookupMetadata<TKey> : ILookupMetadata<TKey>
    {
        public LookupMetadata(
            TKey targetRecordKey,
            bool allowMultipleMatches,
            bool returnMultipleReferences,
            string targetEntityLogicalName,
            string targetAttributeLogicalName,
            string sourceEntityLogicalName,
            string sourceAttributeLogicalName
        )
        {
            TargetRecordKey = targetRecordKey;
            AllowMultipleMatches = allowMultipleMatches;
            ReturnMultipleReferences = returnMultipleReferences;
            TargetEntityLogicalName = targetEntityLogicalName;
            TargetAttributeLogicalName = targetAttributeLogicalName;
            SourceEntityLogicalName = sourceEntityLogicalName;
            SourceAttributeLogicalName = sourceAttributeLogicalName;
        }
        public TKey TargetRecordKey { get; }
        public string TargetKeyType => typeof(TKey).Name;
        public bool AllowMultipleMatches { get; }
        public bool ReturnMultipleReferences { get; }
        public string TargetEntityLogicalName { get; }
        public string TargetAttributeLogicalName { get; }
        public string SourceEntityLogicalName { get; }
        public string SourceAttributeLogicalName { get; }
    }
    public class LookupResult<TKey> : ILookupResult<TKey>
    {
        public LookupResult(
            ILookupMetadata<TKey> metadata,
            LookupMethod method,
            int recordCount,
            bool fromCache,
            TimeSpan duration
        )
        {
            Method = method;
            MatchingRecordCount = recordCount;
            RetrievedFromCache = fromCache;
            LookupDuration = duration;
            Metadata = metadata;
        }
        public ILookupMetadata<TKey> Metadata { get; }
        public LookupMethod Method { get; }
        public int MatchingRecordCount { get; }
        public bool RetrievedFromCache { get; }
        public TimeSpan LookupDuration { get; }
        private readonly List<ILookupError> _errors { get; } = new();
        public IReadOnlyList<ILookupError> Errors => _errors;
        private readonly List<ILookupWarning> _warnings { get; } = new();
        public IReadOnlyList<ILookupWarning> Warnings => _warnings;
        private readonly List<ILookupSuccess> _successes { get; } = new();
        public IReadOnlyList<ILookupSuccess> Successes => _successes;
        public LookupStatus Status
        {
            get
            {
                var warnCount = Warnings.Count;
                var successCount = Successes.Count;
                var errorCount = Errors.Count;

                if (errorCount > 0 && successCount == 0) return LookupStatus.Failed;
                else if (successCount == 0 && errorCount == 0 && warnCount == 0) return LookupStatus.NotAttempted;
                else if (successCount > 0 && warnCount > 0 && errorCount == 0) return LookupStatus.SucceededWithWarnings;
                else if (successCount > 0 && errorCount > 0) return LookupStatus.SucceededWithErrors;
                else if (successCount > 0 && warnCount == 0 && errorCount == 0) return LookupStatus.Succeeded;
                else return LookupStatus.Unknown;
            }
        }
        public bool Succeeded =>
            Status == LookupStatus.Succeeded
            || Status == LookupStatus.SucceededWithWarnings
            || Status == LookupStatus.SucceededWithErrors;

        internal void AddError(string message, LookupFailureReasons reason)
        {
            _errors.Add(new LookupError(message, reason));
        }
        internal void AddError(LookupError error)
        {
            _errors.Add(error);
        }
        internal void AddWarning(string message)
        {
            _warnings.Add(new LookupWarning(message));
        }
        internal void AddWarning(LookupWarning warning)
        {
            _warnings.Add(warning);
        }
        internal void AddSuccess(LookupSuccess success)
        {
            _successes.Add(success);
        }
    }
}