using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class SynStaffMembers
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_synergetic_vstaff_community_leftjoin";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="tracer"></param>
        public SynStaffMembers(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="synEntityId"></param>
        /// <returns></returns>
        public Entity GetEntityById(
            Guid synEntityId,
            int topCount,
            ColumnSet colSet
        )
        {
            try
            {
                _logger.WriteLog("Entered SynQueryService.RetrieveAllMetadata");
                _logger.WriteLog("Constructing QueryExpression");
                _logger.WriteLog("Called with synEntityId: {0}", synEntityId.ToString());

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
                    _logger.WriteLog("Unable to retrieve Synergetic Staff Member using EntityId: {0}. Returning blank Entity.", synEntityId.ToString());
                    return new Entity(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in SynQueryService.RetrieveAllMetadata: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("InnerException in SynQueryService.RetrieveAllMetadata: {0}", ex.InnerException.ToString());
                }
                throw;
            }
        }
    }
}