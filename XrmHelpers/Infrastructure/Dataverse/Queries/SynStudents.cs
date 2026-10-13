using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
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
        private readonly IDataverseLogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="tracer"></param>
        public SynStudents(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<SynStudents>();
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
                _logger.WriteLog($"Constructing QueryExpression with synEntityId: {synEntityId.ToString()}");

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

                var results = _context.Service.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0];
                }
                else
                {
                    _logger.WriteLog($"Unable to retrieve Synergetic Student using EntityId: {synEntityId}. Returning blank Entity.");
                    return new Entity(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: ", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog($"InnerException: {ex.InnerException}");
                }
                throw;
            }
        }
        /*
        public (EntityCollection results, TimeSpan duration) FindByStudentId(IQuery<int> query)
        {
            try
            {
                var entity = query.Metadata.TargetEntityLogicalName;
                var key = query.Metadata.TargetRecordKey;
                var attribute = query.Metadata.TargetAttributeLogicalName;
                var count = query.TopCount;
                var columns = query.Columns;

                _logger.WriteLog($"Querying TABLE: {entity ?? "<NULL>"}, KEY: {key.ToString() ?? "<NULL>"}, TARGET_ATTRIBUTE: {attribute ?? "<NULL>"} TOP_COUNT: {count ?? "<NULL>"}");

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

                var results = _context.Service.RetrieveMultiple(queryExpression);

                return (results, TimeSpan.Zero);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: ", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog($"InnerException: {ex.InnerException}");
                }
                throw;
            }
        }*/
    }
}