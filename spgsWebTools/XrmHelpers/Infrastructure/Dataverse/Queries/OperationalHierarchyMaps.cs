using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class OperationalHierarchyMaps
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.OperationalHierarchyMap;
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public OperationalHierarchyMaps(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetDefaultByCoreJobPositionId(Guid childPositionId, Guid parentPositionId)
        {
            // check if syn reports to position is defined for the given Core Job Position
            try
            {
                _tracer.Trace("Building GetDefaultByCoreJobPositionId query expression to return EntityReference on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns,
                    Criteria = new FilterExpression(LogicalOperator.And)
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.ChildJobPosition,
                                ConditionOperator.Equal,
                                childPositionId
                            ),
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.ParentJobPosition,
                                ConditionOperator.Equal,
                                parentPositionId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0].ToEntityReference();
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return new EntityReference();
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in OperationalHierarchyMaps.GetDefaultByCoreJobPositionId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in OperationalHierarchyMaps.GetDefaultByCoreJobPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public List<Entity> GetOperationalMapsByAssignmentId(Guid assignmentId)
        {
            // check if syn reports to position is defined for the given Core Job Position
            try
            {
                _tracer.Trace("Building GetOperationalMapsByAssignmentId query expression to return EntityReference on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = OperationalHierarchyMapSchema.SyncColumns,
                    Criteria = new FilterExpression(LogicalOperator.And)
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                OperationalHierarchyMapFields.CoreJobAssignment,
                                ConditionOperator.Equal,
                                assignmentId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities.ToList();
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return new List<Entity>();
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in OperationalHierarchyMaps.GetOperationalMapsByAssignmentId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in OperationalHierarchyMaps.GetOperationalMapsByAssignmentId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}