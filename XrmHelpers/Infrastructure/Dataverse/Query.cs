using System.Diagnostics;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class Query<TKey> : IQuery<TKey>
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public Query(
            ILookupMetadata<TKey> metadata,
            IDataverseContext context
        )
        {
            Metadata = metadata;
            _context = context;

            _logger = _context.GetLogger<Query<TKey>>();
        }
        public ILookupMetadata<TKey> Metadata { get; }
        public QueryExpression Build()
        {
            try
            {
                _logger.WriteLog("Building new QueryExpression...");

                QueryExpression queryExpression = new QueryExpression(Metadata.TargetEntityLogicalName)
                {
                    ColumnSet = Metadata.Columns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                Metadata.TargetAttributeLogicalName,
                                ConditionOperator.Equal,
                                Metadata.TargetRecordKey
                            )
                        }
                    }
                };

                _logger.WriteLog("Query building complete...");
                return queryExpression;
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: ", ex);
                throw;
            }
        }
        public (EntityCollection results, TimeSpan duration) Execute()
        {
            try
            {
                _logger.WriteLog("Calling Build() to create QueryExpression...");

                var query = Build();

                _logger.WriteLog("Executing QueryExpression using IOrganizationService provided by IDataverseContext...");

                var stopwatch = Stopwatch.StartNew();
                var results = _context.Runtime.UserService.RetrieveMultiple(query);
                stopwatch.Stop();

                _logger.WriteLog($"Execution complete [Duration: {stopwatch.Elapsed}]...");

                return (results, stopwatch.Elapsed);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: ", ex);
                throw;
            }
        }
    }
}