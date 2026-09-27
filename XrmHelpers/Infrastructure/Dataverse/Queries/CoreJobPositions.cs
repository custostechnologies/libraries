using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreJobPositions
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corejobposition";
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        public CoreJobPositions(IDataverseContext context, DataverseLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public EntityReference GetReferenceBySeq(int seq)
        {
            _logger.WriteLog("Building GetReferenceBySeq query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobPositionFields.JobPositionSeq,
                    CoreJobPositionFields.SourceOfTruthId,
                    CoreJobPositionFields.CoreJobPositionId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobPositionFields.JobPositionSeq,
                            ConditionOperator.Equal,
                            seq
                        )
                    }
                }
            };

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            _logger.WriteLog("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobPositionFields.JobPositionSeq,
                    CoreJobPositionFields.SourceOfTruthId,
                    CoreJobPositionFields.CoreJobPositionId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobPositionFields.CoreJobPositionId,
                            ConditionOperator.Equal,
                            id
                        )
                    }
                }
            };

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public string GetEntityNameById(Guid id)
        {
            _logger.WriteLog("Building GetEntityNameById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(CoreJobPositionFields.JobTitle),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobPositionFields.CoreJobPositionId,
                            ConditionOperator.Equal,
                            id
                        )
                    }
                }
            };

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return results.Entities[0].GetAttributeValue<string>(CoreJobPositionFields.JobTitle);
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return string.Empty;
            }
        }
        public Entity GetEntityById(Guid id)
        {
            _logger.WriteLog("Building GetEntityById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = CoreJobPositionSchema.SyncColumns,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobPositionFields.CoreJobPositionId,
                            ConditionOperator.Equal,
                            id
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
        public Entity GetEntityByCode(string code)
        {
            _logger.WriteLog("Building GetEntityBycode query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = CoreJobPositionSchema.SyncColumns,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobPositionFields.Code,
                            ConditionOperator.Equal,
                            code
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
        public Entity GetPropertyValues(Guid recordId, string[] attributeNames)
        {
            _logger.WriteLog("Building GetPropertyValue query expression on {0}", ENTITY_LOGICAL_NAME);

            var colSet = new ColumnSet(CoreJobPositionFields.CoreJobPositionId);
            if (attributeNames.Length > 0) colSet.AddColumns(attributeNames);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = colSet,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobPositionFields.CoreJobPositionId,
                            ConditionOperator.Equal,
                            recordId
                        )
                    }
                }
            };

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog("CoreJobPositions.GetPropertyValues for recordId: {0} returned {1} entities.",
                    recordId,
                    results.Entities.Count
                );
                return results.Entities[0];
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new Entity();
            }
        }
    }
}