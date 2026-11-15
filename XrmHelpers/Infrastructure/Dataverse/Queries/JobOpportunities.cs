using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class JobOpportunities
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.JobOpportunity;
        public JobOpportunities(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<JobOpportunities>();
        }
        public Entity GetEntityByCode(int webFormOptionValue)
        {
            try
            {
                _logger.WriteLog("Building GetReferenceByCode query expression on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = new ColumnSet(
                        JobOpportunityFields.JobOpportunityId,
                        JobOpportunityFields.WebFormOptionValue,
                        JobOpportunityFields.HiringFor

                    ),
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                JobOpportunityFields.WebFormOptionValue,
                                ConditionOperator.Equal,
                                webFormOptionValue
                            )
                        }
                    }
                };

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0];
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new Entity();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in CoreDepartments.GetReferenceByCode: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in CoreDepartments.GetReferenceByCode: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}