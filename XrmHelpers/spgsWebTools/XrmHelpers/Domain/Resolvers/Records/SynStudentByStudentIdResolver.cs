using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Resolvers
{
    public class SynStudentByStudentIdResolver : ILookupResolver<int>
    {
        private readonly SynStudents _queryProvider;
        private readonly IDataverseContext _context;
        private DataverseLogger _logger;
        public SynStudentByStudentIdResolver(
            IDataverseContext dataverseContext,
            SynStudents queryProvider
        )
        {
            _context = dataverseContext;
            _queryProvider = queryProvider;
            _logger = new DataverseLogger(_context.Tracer, nameof(SynStudentByStudentIdResolver));
        }
        public bool CanResolve(ILookupMetadata<int> metadata)
        {
            return metadata.TargetEntityLogicalName == EntityLogicalNames.SynStudent && metadata.TargetAttributeLogicalName == SynStudentFields.StudentId;
        }
        public ILookupResult<int> Resolve(ILookupMetadata<int> metadata)
        {
            _logger.WriteLog("Starting Resolve method and building query using query provider...");

            var cols = new ColumnSet(
                SynStudentColumns.SynMirrorId,
                SynStudentColumns.StudentId
            );

            var query = new Query<int>(
                metadata.TargetEntityLogicalName,
                metadata.TargetRecordKey,
                metadata.TargetAttributeLogicalName,
                cols,
                null // allow RetrieveMultiple to return more than one matching record
            );

            _logger.WriteLog("Executing query...");

            RetrieveMultipleResponse response = _queryProvider.FindByStudentId(query);

            _logger.WriteLog("RetrieveMultipleResponse received from queryProvider, conducting LookupResult validation...");

            var recordCount = response.Entities.Count;
            var allowMultiple = metadata.AllowMultipleMatches;

            var luResult = new LookupResult<int>(
                metadata,
                LookupMethods.NonKeyMatch,
                recordCount,
                false,
                (TimeSpan)0
            );

            if (luResult == null)
            {
                _logger.WriteLog("LookupResult is null after construction attempt...");
                throw new InvalidPluginExecutionException();
            }

            if (recordCount == 0)
            {
                luResult.AddError(
                    "Lookup query references.Count: {0}",
                    LookupFailureReasons.RecordNotFound
                );
            }
            else if (recordCount == 1)
            {
                luResult.AddSuccess(
                    response.Entities[0].ToEntityReference()
                );
            }
            else
            {
                if (allowMultiple == false)
                {
                    var err = LookupFailureReaons.MultipleMatchesFound;
                    luResult.AddError(
                        $"[] Lookup query references.Count: {recordCount}, Error Message: {err}",
                        err
                    );
                }
                else
                {
                    List<EntityReference> successes = new List<EntityReference>();

                    foreach (Entity entity in response.Entities)
                    {
                        if (entity.Id == Guid.Empty)
                        {
                            var err = LookupFailureReasons.RecordNotFound;
                            luResult.AddError(
                                $"[] {err}",
                                err
                            );
                        }
                        else
                        {
                            successes.Add(entity.ToEntityReference());
                        }
                    }

                    luResult.AddRange(successes);
                }
            }

            _logger.WriteLog("Completed LookupResult construction and validation; returning object to caller.");
            return luResult;
        }
    }
}