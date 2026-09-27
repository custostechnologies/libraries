using System.Security.Claims;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ReferenceHierarchyMaps
    {
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.ReferenceHierarchyMap;
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        public ReferenceHierarchyMaps(IDataverseContext context, DataverseLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<EntityReference> GetDirectReportsByPositionId(Guid coreJobPositionId)
        {
            try
            {
                _logger.WriteLog("Building GetDirectReportsByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);

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

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities
                        .Select(entity => entity.ToEntityReference())
                        .ToList();
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new List<EntityReference>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public List<EntityReference> GetReportsToByPositionId(Guid coreJobPositionId)

        {
            try
            {
                _logger.WriteLog("Building GetReportsToByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);

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

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities
                        .Select(entity => entity.ToEntityReference())
                        .ToList();
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new List<EntityReference>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public EntityReference GetReferenceMapBySynReportsTo(string reportsTo)
        {
            try
            {
                _logger.WriteLog("Building GetReportsToByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);

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

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0].ToEntityReference();
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new EntityReference(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in ReferenceHierarchyMaps.GetReportsToByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public List<EntityReference> GetReferenceMapsByPositionId(Guid coreJobPositionId, Enums.Campus? campus)
        {
            try
            {
                _logger.WriteLog("Building GetReferenceMapsByPositionId query expression to return EntityReferenceCollection on {0}", ENTITY_LOGICAL_NAME);
                if (campus != null) _logger.WriteLog("CAMPUS value passed in to method call, using as a LogicalOperator.And CONDITION");
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

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities
                        .Select(entity => entity.ToEntityReference())
                        .ToList();
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new List<EntityReference>();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in ReferenceHierarchyMaps.GetReferenceMapsByPositionId: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in ReferenceHierarchyMaps.GetReferenceMapsByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
        public Entity GetEntityById(Guid referenceMapId)
        {
            try
            {
                _logger.WriteLog("Building GetEntityById query expression to return Entity on {0}", ENTITY_LOGICAL_NAME);

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

                _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression");

                if (results.Entities.Count > 0)
                {
                    return results.Entities[0];
                }
                else
                {
                    _logger.WriteLog("No existing entities were found");
                    return new Entity(ENTITY_LOGICAL_NAME);
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex);
                if (ex.InnerException != null)
                {
                    _logger.WriteLog("Inner Exception in ReferenceHierarchyMaps.GetDirectReportsByPositionId: {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}