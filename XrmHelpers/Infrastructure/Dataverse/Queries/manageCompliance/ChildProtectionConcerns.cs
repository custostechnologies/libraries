using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ChildProtectionConcerns
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.ChildProtectionConcern;
        public ChildProtectionConcerns(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<JobOpportunities>();
        }
        public int GetMatchingCount(int studentId)
        {
            try
            {
                _logger.WriteLog("Building GetMatchingCount query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        ChildProtectionConcernFields.ChildProtectionConcernId,
                        ChildProtectionConcernFields.CommunityId
                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ChildProtectionConcernFields.CommunityId,
                                ConditionOperator.Equal,
                                studentId.ToString()
                            )
                        }
                    }
                };

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                return results.Entities.Count;
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in ChildProtectionConcerns.GetMatchingCount: {0}", ex);
                throw ex;
            }
        }
    }
}