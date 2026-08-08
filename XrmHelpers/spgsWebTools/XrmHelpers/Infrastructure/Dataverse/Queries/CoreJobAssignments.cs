using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;
using Enums = spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class CoreJobAssignments
    {
        private readonly string ENTITY_LOGICAL_NAME = "mthomas_corejobassignment";
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracer;
        public CoreJobAssignments(IOrganizationService service, ITracingService tracer)
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
                    CoreJobAssignmentFields.StaffJobPositionSeq,
                    CoreJobAssignmentFields.SourceOfTruthId,
                    CoreJobAssignmentFields.CoreJobAssignmentId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.StaffJobPositionSeq,
                            ConditionOperator.Equal,
                            seq
                        )
                    }
                }
            };

            _tracer.Trace("Executing query expression on {0} with STAFF JOB POSITION SEQ: {1}", ENTITY_LOGICAL_NAME, seq);

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
        public EntityReference GetReferenceByTruth(string truth)
        {
            _tracer.Trace("Building GetReferenceByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobAssignmentFields.StaffJobPositionSeq,
                    CoreJobAssignmentFields.SourceOfTruthId,
                    CoreJobAssignmentFields.CoreJobAssignmentId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.SourceOfTruthId,
                            ConditionOperator.Equal,
                            truth
                        )
                    }
                }
            };

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(
                    ENTITY_LOGICAL_NAME,
                    results.Entities[0].Id
                );
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public Entity GetEntityByTruth(string truth)
        {
            _tracer.Trace("Building GetEntityByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobAssignmentFields.StaffJobPositionSeq,
                    CoreJobAssignmentFields.SourceOfTruthId,
                    CoreJobAssignmentFields.CoreJobAssignmentId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.SourceOfTruthId,
                            ConditionOperator.Equal,
                            truth
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
        public Entity GetEntityBySeq(int seq)
        {
            _tracer.Trace("Building GetEntityByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobAssignmentFields.StaffJobPositionSeq,
                    CoreJobAssignmentFields.SourceOfTruthId,
                    CoreJobAssignmentFields.CoreJobAssignmentId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.StaffJobPositionSeq,
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
                return new Entity(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new Entity(ENTITY_LOGICAL_NAME);
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            _tracer.Trace("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobAssignmentFields.StaffJobPositionSeq,
                    CoreJobAssignmentFields.SourceOfTruthId,
                    CoreJobAssignmentFields.CoreJobAssignmentId

                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.CoreJobAssignmentId,
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
        public Entity GetEntityByPositionId(Guid id)
        {
            // should throw error if more than one entity is found
            _tracer.Trace("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = CoreJobAssignmentSchema.SyncColumns,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.CoreJobPosition,
                            ConditionOperator.Equal,
                            id
                        )
                    }
                }
            };

            _tracer.Trace("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _service.RetrieveMultiple(query);

            _tracer.Trace("Finished executing query expression");

            if (results.Entities.Count == 0)
            {
                return results.Entities[0];
            }
            else if (results.Entities.Count > 0)
            {
                _tracer.Trace("More than one Core Job Assignment returned.");
                return new Entity(ENTITY_LOGICAL_NAME);
            }
            else
            {
                _tracer.Trace("No existing entities were found");
                return new Entity(ENTITY_LOGICAL_NAME);
            }
        }
        public EntityReference GetMostRecentReferenceByPositionId(Guid jobPositionId)
        {
            _tracer.Trace("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    CoreJobAssignmentFields.StaffJobPositionSeq,
                    CoreJobAssignmentFields.SourceOfTruthId,
                    CoreJobAssignmentFields.CoreJobAssignmentId

                ),
                Orders =
                {
                    new OrderExpression(
                        CoreJobAssignmentFields.StaffJobPositionSeq,
                        OrderType.Descending
                    )
                },
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            CoreJobAssignmentFields.CoreJobAssignmentId,
                            ConditionOperator.Equal,
                            jobPositionId
                        ),
                        new ConditionExpression(
                            CoreJobAssignmentFields.Status,
                            ConditionOperator.Equal,
                            new OptionSetValue((int)Enums.Status.Active)
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
    }
}