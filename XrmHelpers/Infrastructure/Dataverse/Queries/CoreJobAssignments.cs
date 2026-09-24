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
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        public CoreJobAssignments(
            IDataverseContext context
        )
        {
            _context = context;
            _logger = new DataverseLogger(
                _context.Logger,
                nameof(CoreJobAssignments)
            );
        }
        public EntityReference GetReferenceBySeq(int seq)
        {
            _logger.WriteLog("Building GetReferenceBySeq query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _logger.WriteLog($"Executing query expression on {ENTITY_LOGICAL_NAME} with STAFF JOB POSITION SEQ: {seq}");

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
        public EntityReference GetReferenceByTruth(string truth)
        {
            _logger.WriteLog("Building GetReferenceByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new EntityReference(
                    ENTITY_LOGICAL_NAME,
                    results.Entities[0].Id
                );
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new EntityReference(ENTITY_LOGICAL_NAME);
            }
        }
        public Entity GetEntityByTruth(string truth)
        {
            _logger.WriteLog("Building GetEntityByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

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
        public Entity GetEntityBySeq(int seq)
        {
            _logger.WriteLog("Building GetEntityByTruth query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count > 0)
            {
                return new Entity(ENTITY_LOGICAL_NAME, results.Entities[0].Id);
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new Entity(ENTITY_LOGICAL_NAME);
            }
        }
        public EntityReference GetReferenceById(Guid id)
        {
            _logger.WriteLog("Building GetReferenceById query expression on {0}", ENTITY_LOGICAL_NAME);

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
        public Entity GetEntityByPositionId(Guid id)
        {
            // should throw error if more than one entity is found
            _logger.WriteLog("Building GetEntityByPositionId query expression on {0}", ENTITY_LOGICAL_NAME);

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

            _logger.WriteLog("Executing query expression on {0}", ENTITY_LOGICAL_NAME);

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression");

            if (results.Entities.Count == 0)
            {
                return results.Entities[0];
            }
            else if (results.Entities.Count > 0)
            {
                _logger.WriteLog("More than one Core Job Assignment returned.");
                return new Entity(ENTITY_LOGICAL_NAME);
            }
            else
            {
                _logger.WriteLog("No existing entities were found");
                return new Entity(ENTITY_LOGICAL_NAME);
            }
        }
        public Entity GetEntityById(Guid id)
        {
            // should throw error if more than one entity is found
            _logger.WriteLog("Building GetEntityById query expression on {0}", ENTITY_LOGICAL_NAME);

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = CoreJobAssignmentSchema.SyncColumns,
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
        public EntityReference GetMostRecentReferenceByPositionId(Guid jobPositionId)
        {
            _logger.WriteLog("Building GetMostRecentReferenceByPositionId query expression on {0}", ENTITY_LOGICAL_NAME);
            _logger.WriteLog($"With jobPositionId={jobPositionId}");

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
                            CoreJobAssignmentFields.CoreJobPosition,
                            ConditionOperator.Equal,
                            jobPositionId
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
    }
}