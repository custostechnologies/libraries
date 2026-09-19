<<<<<<< HEAD
=======
using System.Security.Claims;
>>>>>>> source-temp/refactor/decouple-xrmhelpers
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;
<<<<<<< HEAD
=======
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
>>>>>>> source-temp/refactor/decouple-xrmhelpers

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ReferenceHierarchyMaps
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.ReferenceHierarchyMap;
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public ReferenceHierarchyMaps(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public List<EntityReference> GetDirectReportsByPositionId(Guid coreJobPositionId)
        {
            try
            {
                _tracer.Trace("Building GetDirectReportsByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.ParentJobPosition,
                                ConditionOperator.Equal,
                                coreJobPositionId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities
                        .Select(entity => entity.ToEntityReference())
                        .ToList();
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return new List<EntityReference>();
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public List<EntityReference> GetReportsToByPositionId(Guid coreJobPositionId)

        {
            try
            {
                _tracer.Trace("Building GetReportsToByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.ChildJobPosition,
                                ConditionOperator.Equal,
                                coreJobPositionId
                            )
                        }
                    }
                };

                _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities
                        .Select(entity => entity.ToEntityReference())
                        .ToList();
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return new List<EntityReference>();
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public EntityReference GetReferenceMapBySynReportsTo(string reportsTo)
        {
            try
            {
                _tracer.Trace("Building GetReportsToByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.ChildJobPosition,
                                ConditionOperator.Equal,
                                reportsTo
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
                    return new EntityReference(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
<<<<<<< HEAD
        public List<EntityReference> GetReferenceMapsByPositionId(Guid coreJobPositionId)
=======
        public List<EntityReference> GetReferenceMapsByPositionId(Guid coreJobPositionId, Enums.Campus? campus)
>>>>>>> source-temp/refactor/decouple-xrmhelpers
        {
            try
            {
                _tracer.Trace("Building GetReferenceMapsByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);
<<<<<<< HEAD

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.CoreJobPosition,
                                ConditionOperator.Equal,
                                coreJobPositionId
                            )
                        }
                    }
                };

=======
                if (campus != null) _tracer.Trace("CAMPUS value passed in to method call, using as a LogicalOperator.And CONDITION");
                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns
                };

                query.Criteria.AddCondition(
                    ReferenceHierarchyMapFields.CoreJobPosition,
                    ConditionOperator.Equal,
                    coreJobPositionId
                );

                if (campus != null)
                {
                    query.Criteria.AddCondition(
                        ReferenceHierarchyMapFields.Campus,
                        ConditionOperator.Equal,
                        (int)campus
                    );
                }

>>>>>>> source-temp/refactor/decouple-xrmhelpers
                _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _service.RetrieveMultiple(query);

                _tracer.Trace("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities
                        .Select(entity => entity.ToEntityReference())
                        .ToList();
                }
                else
                {
                    _tracer.Trace("No existing entities were found");
                    return new List<EntityReference>();
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in ReferenceHierarchyMaps.GetReferenceMapsByPositionId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in ReferenceHierarchyMaps.GetReferenceMapsByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public Entity GetEntityById(Guid referenceMapId)
        {
            try
            {
                _tracer.Trace("Building GetEntityById query expression to return Entity on {0}", ENTITY_LOGICAL_NAME);

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    TopCount = 1,
                    ColumnSet = ReferenceHierarchyMapSchema.AllSyncColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ReferenceHierarchyMapFields.ReferenceHierarchyMapId,
                                ConditionOperator.Equal,
                                referenceMapId
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
                    return new Entity(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _tracer.Trace("Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _tracer.Trace("Inner Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}