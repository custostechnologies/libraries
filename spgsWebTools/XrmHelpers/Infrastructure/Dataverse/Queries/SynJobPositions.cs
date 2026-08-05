using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class SynJobPositions
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_synergetic_vstaffjobpositions";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="tracer"></param>
        public SynJobPositions(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="synEntityId"></param>
        /// <returns></returns>
        public Entity GetMetadataById(
            Guid synEntityId,
            int topCount,
            ColumnSet colSet
        )
        {
            try
            {
                _tracer.Trace("Entered SynQueryService.RetrieveAllMetadata");
                _tracer.Trace("Constructing QueryExpression");
                _tracer.Trace("Called with synEntityId: {0}", synEntityId.ToString());

                QueryExpression query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = topCount,
                    ColumnSet = colSet,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                $"{ENTITY_LOGICAL_NAME}id",
                                ConditionOperator.Equal,
                                synEntityId
                            )
                        }
                    }
                };

                var results = _service.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0];
                }
                else
                {
                    _tracer.Trace("Unable to retrieve Synergetic Job Position using EntityId: {0}. Returning blank Entity.", synEntityId.ToString());
                    return new Entity(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in SynQueryService.RetrieveAllMetadata: {0}", ex);
                if (ex.InnerException != null)
                {
                    _tracer.Trace("InnerException in SynQueryService.RetrieveAllMetadata: {0}", ex.InnerException.ToString());
                }
                throw;
            }
        }
    }
}