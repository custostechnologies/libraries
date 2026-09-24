using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class JobOpportunities
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.JobOpportunity;
        public JobOpportunities(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public Entity GetEntityByCode(int webFormOptionValue)
        {
            try
            {
                _tracer.Trace("Building GetReferenceByCode query expression on {0}", ENTITY_LOGICAL_NAME);

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

                _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0];
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in CoreDepartments.GetReferenceByCode: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in CoreDepartments.GetReferenceByCode: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}