using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class LookupError : ILookupError
    {
        public LookupError(string message, LuFailureReason reason)
        {
            Message = message;
            Reason = reason;
        }
        public string Message { get; }
        public LuFailureReason Reason { get; }
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
        public LookupSuccess(List<EntityReference> refs)
        {
            References = refs;
        }
        public List<EntityReference> References { get; }
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
            string sourceAttributeLogicalName,
            ColumnSet columnSet,
            int topCount
        )
        {
            TargetRecordKey = targetRecordKey;
            AllowMultipleMatches = allowMultipleMatches;
            ReturnMultipleReferences = returnMultipleReferences;
            TargetEntityLogicalName = targetEntityLogicalName;
            TargetAttributeLogicalName = targetAttributeLogicalName;
            SourceEntityLogicalName = sourceEntityLogicalName;
            SourceAttributeLogicalName = sourceAttributeLogicalName;
            Columns = columnSet;
            TopCount = topCount;
        }
        public TKey TargetRecordKey { get; }
        object ILookupMetadata.TargetRecordKey => TargetRecordKey ?? throw new InvalidOperationException("TargetRecordKey cannot be null when evaluating metadata.");
        public string TargetKeyType => typeof(TKey).Name;
        public bool AllowMultipleMatches { get; }
        public bool ReturnMultipleReferences { get; }
        public string TargetEntityLogicalName { get; }
        public string TargetAttributeLogicalName { get; }
        public string SourceEntityLogicalName { get; }
        public string SourceAttributeLogicalName { get; }
        public ColumnSet Columns { get; }
        public int TopCount { get; } = 500;
    }
    public class LookupResult<TKey> : ILookupResult<TKey>
    {
        public LookupResult(
            ILookupMetadata<TKey> metadata,
            LuMethod method,
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
        public LuMethod Method { get; }
        public int MatchingRecordCount { get; }
        public bool RetrievedFromCache { get; }
        public TimeSpan LookupDuration { get; }
        private List<ILookupError> _errors { get; } = new();
        public IReadOnlyList<ILookupError> Errors => _errors;
        private List<ILookupWarning> _warnings { get; } = new();
        public IReadOnlyList<ILookupWarning> Warnings => _warnings;
        private List<ILookupSuccess> _successes { get; } = new();
        public IReadOnlyList<ILookupSuccess> Successes => _successes;
        public LuStatus Status
        {
            get
            {
                var warnCount = Warnings.Count;
                var successCount = Successes.Count;
                var errorCount = Errors.Count;

                if (errorCount > 0 && successCount == 0) return LuStatus.Failed;
                else if (successCount == 0 && errorCount == 0 && warnCount == 0) return LuStatus.NotAttempted;
                else if (successCount > 0 && warnCount > 0 && errorCount == 0) return LuStatus.SucceededWithWarnings;
                else if (successCount > 0 && errorCount > 0) return LuStatus.SucceededWithErrors;
                else if (successCount > 0 && warnCount == 0 && errorCount == 0) return LuStatus.Succeeded;
                else return LuStatus.Unknown;
            }
        }
        public bool Succeeded =>
            Status == LuStatus.Succeeded
            || Status == LuStatus.SucceededWithWarnings
            || Status == LuStatus.SucceededWithErrors;

        internal void AddError(string message, LuFailureReason reason)
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
        internal void AddSuccessRange(List<EntityReference> references)
        {
            _successes.Add(new LookupSuccess(references));
        }
        internal void AddSuccess(EntityReference reference)
        {
            List<EntityReference> list = new() { reference };
            _successes.Add(new LookupSuccess(list));
        }
        public string GenerateDetailedLogSummary()
        {
            var errors = Errors.Count > 0
                ? string.Join(
                    "; ",
                    Errors.Select(e => $"[{e.Reason}] {e.Message}"))
                : "None";

            var warnings = Warnings.Count > 0
                ? string.Join(
                    "; ",
                    Warnings.Select(w => w.Message))
                : "None";

            var successes = Successes.Count > 0
                ? string.Join(
                    "; ",
                    Successes.SelectMany(s => s.References)
                        .Select(r => $"{r.LogicalName}:{r.Id}"))
                : "None";

            return $@"
                === Lookup Summary ===
                Status: {Status}
                Method: {Method}

                Input Parameters
                ----------------
                Target Key Type: {Metadata.TargetKeyType}
                Target Key Value: {Metadata.TargetRecordKey}
                Target Entity: {Metadata.TargetEntityLogicalName}
                Target Attribute: {Metadata.TargetAttributeLogicalName}
                Source Entity: {Metadata.SourceEntityLogicalName}
                Source Attribute: {Metadata.SourceAttributeLogicalName}
                Allow Multiple Matches: {Metadata.AllowMultipleMatches}
                Return Multiple References: {Metadata.ReturnMultipleReferences}
                Top Count: {Metadata.TopCount}

                Execution
                ---------
                Records Found: {MatchingRecordCount}
                Retrieved From Cache: {RetrievedFromCache}
                Duration: {LookupDuration}

                Results
                -------
                Success Count: {Successes.Count}
                Warning Count: {Warnings.Count}
                Error Count: {Errors.Count}

                Successes
                ---------
                {successes}

                Warnings
                --------
                {warnings}

                Errors
                ------
                {errors}";
        }
        public string GenerateLogSummary()
        {
            return
                $"Status={Status}; " +
                $"Method={Method}; " +
                $"Records={MatchingRecordCount}; " +
                $"Duration={LookupDuration.TotalMilliseconds:N0}ms; " +
                $"Entity={Metadata.TargetEntityLogicalName}; " +
                $"Attribute={Metadata.TargetAttributeLogicalName}; " +
                $"Key={Metadata.TargetRecordKey}; " +
                $"Successes={Successes.Count}; " +
                $"Warnings={Warnings.Count}; " +
                $"Errors={Errors.Count}";
        }
    }
}