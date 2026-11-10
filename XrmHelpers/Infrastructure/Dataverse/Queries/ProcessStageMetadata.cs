using System;
using System.Text.Json;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Schemas;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ProcessStageMetadata
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.ProcessStageMetadata;
        public ProcessStageMetadata(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<ProcessStageMetadata>();
        }
        public EntityReference GetParentProcessRef(string processKey)
        {
            _logger.WriteLog($"Building query expression on pgrowth_managementprocessconfiguration");

            var safeKey = SafeString.Normalise(processKey);
            var query = new QueryExpression("pgrowth_managementprocessconfiguration")
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    "pgrowth_managementprocessconfigurationid",
                    "pgrowth_processkey"
                ),
                Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                "pgrowth_processkey",
                                ConditionOperator.Equal,
                                safeKey
                            )
                        }
                    }
            };

            _logger.WriteLog($"Executing query expression on pgrowth_managementprocessconfiguration");

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression, interpreting results...");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog($"{results.Entities.Count} entities returned. Returning results.Entities[0].");
                return new EntityReference("pgrowth_managementprocessconfiguration", results.Entities[0].Id);
            }
            else
            {
                throw new InvalidPluginExecutionException($"Unable to resolve Guid for Management Process Configuration record with key: {safeKey}");
            }
        }
        public EntityReference GetStageRef(EntityReference parentRef, string stageKey)
        {
            _logger.WriteLog($"Building query expression on {ENTITY_LOGICAL_NAME}");
            _logger.WriteLog($"Executing GetStageRef with parentRef logicalName: {parentRef.LogicalName}, id: {parentRef.Id} and stageKey: {stageKey}");

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(ProcessStageMetadataFields.ParentBusinessProcessFlow, ProcessStageMetadataFields.AssignedTeam),
                Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ProcessStageMetadataFields.ParentBusinessProcessFlow,
                                ConditionOperator.Equal,
                                parentRef.Id
                            ),
                            new ConditionExpression(
                                ProcessStageMetadataFields.StageKey,
                                ConditionOperator.Equal,
                                stageKey
                            ),
                            new ConditionExpression(
                                ProcessStageMetadataFields.Status,
                                ConditionOperator.Equal,
                                (int)Status.Active
                            )
                        }
                    }
            };

            _logger.WriteLog($"Executing query expression on {ENTITY_LOGICAL_NAME}");

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression, interpreting results...");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog($"{results.Entities.Count} entities returned. Returning results.Entities[0].toEntityReference).");

                return results.Entities[0].ToEntityReference();
            }
            else
            {
                throw new InvalidPluginExecutionException(
                    $"Unable to resolve AssignedTeam for Process Stage with parent ID: {parentRef.Id}, stageKey: {stageKey}"
                );
            }
        }
        public Guid GetProcessStageId(EntityReference metadataStageRef)
        {
            _logger.WriteLog($"Retrieving GetProcessStageId from {ENTITY_LOGICAL_NAME}");

            _logger.WriteLog($"metadataStageRef id: {metadataStageRef.Id}, logicalName: {metadataStageRef.LogicalName}");

            var stage = _context.Runtime.UserService.Retrieve(
                metadataStageRef.LogicalName,
                metadataStageRef.Id,
                new ColumnSet("pgrowth_stageidx")
            );

            _logger.WriteLog("Finished retrieval, interpreting results...");

            if (stage == null)
            {
                throw new InvalidPluginExecutionException($"Unable to retrieve metadata stage.");
            }
            else
            {
                var idx = stage.GetAttributeValue<string>("pgrowth_stageidx");
                _logger.WriteLog($"Returning idx {idx} to caller");
                return Guid.Parse(idx);
            }
        }
        public Entity? GetProcessStageMetadata(EntityReference metadataStageRef)
        {
            _logger.WriteLog($"Retrieving GetProcessStageId from {ENTITY_LOGICAL_NAME}");

            _logger.WriteLog($"metadataStageRef id: {metadataStageRef.Id}, logicalName: {metadataStageRef.LogicalName}");

            var stage = _context.Runtime.UserService.Retrieve(
                metadataStageRef.LogicalName,
                metadataStageRef.Id,
                new ColumnSet(
                    ProcessStageMetadataFields.StageIdx,
                    ProcessStageMetadataFields.StageKey,
                    ProcessStageMetadataFields.ParentBusinessProcessFlow,
                    ProcessStageMetadataFields.ConditionExpression,
                    ProcessStageMetadataFields.Index,
                    ProcessStageMetadataFields.AssignedTeam
                )
            );

            _logger.WriteLog("Finished retrieval, interpreting results...");

            if (stage == null)
            {
                throw new InvalidPluginExecutionException($"Unable to retrieve metadata stage.");
            }
            else
            {
                _logger.WriteLog($"Returning stage entity with id {stage.Id} to caller");
                return stage;
            }
        }
        public Entity? GetProcessStageMetadata(Guid processStageId)
        {
            _logger.WriteLog($"Retrieving GetProcessStageMetadata from {ENTITY_LOGICAL_NAME}");

            _logger.WriteLog($"processStageId: {processStageId}");

            QueryExpression query = new QueryExpression(EntityLogicalNames.ProcessStageMetadata)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(
                    ProcessStageMetadataFields.StageIdx,
                    ProcessStageMetadataFields.StageKey,
                    ProcessStageMetadataFields.ParentBusinessProcessFlow,
                    ProcessStageMetadataFields.ConditionExpression,
                    ProcessStageMetadataFields.Index,
                    ProcessStageMetadataFields.AssignedTeam
                ),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            ProcessStageMetadataFields.StageIdx,
                            ConditionOperator.Equal,
                            processStageId.ToString()
                        )
                    }
                }
            };

            var results = _context.Runtime.UserService.RetrieveMultiple(query);
            _logger.WriteLog("Finished retrieval, interpreting results...");

            if (results.Entities.Count == 0)
            {
                throw new InvalidPluginExecutionException($"Unable to retrieve metadata stage.");
            }
            else
            {
                var entity = results.Entities[0];
                _logger.WriteLog($"Returning metadata for entity with id {entity.Id} to caller");
                return entity;
            }
        }
        public EntityReference GetAssignedTeam(EntityReference processStageRef)
        {
            _logger.WriteLog($"Building GetAssignedTeam query expression on {ENTITY_LOGICAL_NAME} using processStageRef");
            _logger.WriteLog($"processStageRef logicalName: {processStageRef.LogicalName}, id: {processStageRef.Id}");

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(ProcessStageMetadataFields.ParentBusinessProcessFlow, ProcessStageMetadataFields.AssignedTeam),
                Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ProcessStageMetadataFields.StageIdx,
                                ConditionOperator.Equal,
                                processStageRef.Id.ToString()
                            )
                        }
                    }
            };

            _logger.WriteLog($"Executing query expression on {ENTITY_LOGICAL_NAME}");

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression, interpreting results...");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog($"{results.Entities.Count} entities returned. Returning results.Entities[0].teamRef.");

                var teamRef = results.Entities[0].GetAttributeValue<EntityReference>(ProcessStageMetadataFields.AssignedTeam);
                if (teamRef == null)
                {
                    throw new InvalidPluginExecutionException(
                        $"Process Stage Metadata record {results.Entities[0].Id} has no assigned team."
                    );
                }

                return teamRef;
            }
            else
            {
                throw new InvalidPluginExecutionException(
                    $"Unable to resolve AssignedTeam for Process Stage with ID: {processStageRef.Id}, query returned {results.Entities.Count} entities."
                );
            }
        }
        public EntityReference GetAssignedTeam(Guid processStageMetadataId)
        {
            _logger.WriteLog($"Building GetAssignedTeam query expression on {ENTITY_LOGICAL_NAME}");
            _logger.WriteLog($"processStageMetadataId: {processStageMetadataId}");

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = new ColumnSet(ProcessStageMetadataFields.ParentBusinessProcessFlow, ProcessStageMetadataFields.AssignedTeam),
                Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                ProcessStageMetadataFields.ProcessStageMetadataId,
                                ConditionOperator.Equal,
                                processStageMetadataId
                            )
                        }
                    }
            };

            _logger.WriteLog($"Executing query expression on {ENTITY_LOGICAL_NAME}");

            var results = _context.Runtime.UserService.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression, interpreting results...");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog($"{results.Entities.Count} entities returned. Returning results.Entities[0].teamRef.");

                var teamRef = results.Entities[0].GetAttributeValue<EntityReference>(ProcessStageMetadataFields.AssignedTeam);
                if (teamRef == null)
                {
                    throw new InvalidPluginExecutionException(
                        $"Process Stage Metadata record {results.Entities[0].Id} has no assigned team."
                    );
                }

                return teamRef;
            }
            else
            {
                throw new InvalidPluginExecutionException(
                    $"Unable to resolve AssignedTeam for Process Stage Metadata row with ID: {processStageMetadataId}, query returned {results.Entities.Count} entities."
                );
            }
        }
    }
}