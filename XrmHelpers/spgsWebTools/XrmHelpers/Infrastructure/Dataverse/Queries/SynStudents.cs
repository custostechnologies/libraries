using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public class SynStudents
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.SynStudent;
        private readonly IDataverseContext _context;
        private DataverseLogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="tracer"></param>
        public SynStudents(IDataverseContext context)
        {
            _context = context;

            _logger = new DataverseLogger(_context.Tracer, nameof(SynStudents));
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
                    _tracer.Trace("Unable to retrieve Synergetic Student using EntityId: {0}. Returning blank Entity.", synEntityId.ToString());
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
        public RetrieveMultipleResponse FindByStudentId(IQuery query)
        {
            try
            {
                var entity = query.TargetEntityLogicalName;
                var key = query.TargetRecordKey;
                var attribute = query.TargetAttributeLogicalName;
                var count = query.TopCount;
                var columns = query.Columns;

                _tracer.Trace($"Querying TABLE: {entity ?? "<NULL>"}, KEY: {key ?? "<NULL>"}, TARGET_ATTRIBUTE: {attribute ?? "<NULL>"} TOP_COUNT: {count ?? "<NULL>"}");

                QueryExpression queryExpression = new QueryExpression(entity)
                {
                    ColumnSet = columns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                attribute,
                                ConditionOperator.Equal,
                                key
                            )
                        }
                    }
                };

                if (count != null) queryExpression.TopCount = count;

                var results = _service.RetrieveMultiple(queryExpression);

                return results;
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: {ex}");
                if (ex.InnerException != null)
                {
                    _tracer.Trace($"InnerException: {ex.InnerException.ToString()}");
                }
                throw;
            }
        }
    }
}