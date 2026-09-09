using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreJobPositions
    {
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_corejobposition";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public CoreJobPositions(IOrganizationService service, ITracingService tracer)
        {
            _service = service;
            _tracer = tracer;
        }
        public EntityReference GetReferenceBySeq(int seq)
        {
            _tracer.Trace("Building GetReferenceBySeq query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            _tracer.Trace("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public string GetEntityNameById(Guid id)
        {
            _tracer.Trace("Building GetEntityNameById query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return results.Entities[0].GetAttributeValue<string>(CoreJobPositionFields.JobTitle);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return null;
            }
        }
        public Entity GetEntityById(Guid id)
        {
            _tracer.Trace("Building GetEntityById query expression on {0}", ENTITY_LOGICAL_NAME);

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
        public Entity GetEntityByCode(string code)
        {
            _tracer.Trace("Building GetEntityBycode query expression on {0}", ENTITY_LOGICAL_NAME);

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
        public Entity GetPropertyValues(Guid recordId, string[] attributeNames)
        {
            _tracer.Trace("Building GetPropertyValue query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                _tracer.Trace("CoreJobPositions.GetPropertyValues for recordId: {0} returned {1} entities.",
                    recordId,
                    results.Entities.Count
                );
                return results.Entities[0];
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return null;
            }
        }
    }
}