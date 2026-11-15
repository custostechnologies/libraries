using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Infrastructure.Dataverse
{
    public class LookupResolver<TKey> : ILookupResolver<TKey>
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public LookupResolver(IDataverseContext dataverseContext, IDataverseLoggerFactory loggerFactory)
        {
            _context = dataverseContext;

            _logger = loggerFactory.Create<LookupResolver<TKey>>();
        }
        //public bool CanResolve(ILookupMetadata<TKey> metadata);
        public ILookupResult<TKey> Resolve(ILookupMetadata<TKey> metadata)
        {
            _logger.WriteLog("Starting Resolve method and building query using query provider...");

            var query = new Query<TKey>(
                metadata,
                _context
            );

            var response = query.Execute();

            var duration = response.duration;
            var results = response.results;

            var recordCount = results.Entities.Count;
            var allowMultiple = metadata.AllowMultipleMatches;

            var luResult = new LookupResult<TKey>(
                metadata,
                LuMethod.NoKey,
                recordCount,
                false,
                duration
            );

            if (recordCount == 0)
            {
                luResult.AddError(
                    "Lookup query references.Count: {0}",
                    LuFailureReason.RecordNotFound
                );
            }
            else if (recordCount == 1)
            {
                luResult.AddSuccess(
                    results.Entities[0].ToEntityReference()
                );
            }
            else
            {
                if (allowMultiple == false)
                {
                    var err = LuFailureReason.MultipleMatches;
                    luResult.AddError(
                        $"[] Lookup query references.Count: {recordCount}, Error Message: {err}",
                        err
                    );
                }
                else
                {
                    List<EntityReference> successes = new List<EntityReference>();

                    foreach (Entity entity in results.Entities)
                    {
                        if (entity.Id == Guid.Empty)
                        {
                            var err = LuFailureReason.RecordNotFound;
                            luResult.AddError(
                                $"[] {err.ToString()}",
                                err
                            );
                        }
                        else
                        {
                            successes.Add(entity.ToEntityReference());
                        }
                    }

                    luResult.AddSuccessRange(successes);
                }
            }

            _logger.WriteLog("Completed LookupResult construction and validation; returning object to caller.");
            return luResult;
        }
    }
}