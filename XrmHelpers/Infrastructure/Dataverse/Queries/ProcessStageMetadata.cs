using System;
using System.Text.Json;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ProcessStageMetadata
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = "pgrowth_processstagemetadata";
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
                ColumnSet = new ColumnSet("pgrowth_managementprocessconfigurationid", "pgrowth_processkey"),
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
        public EntityReference GetProcessStageMetadataAssignedTeam(EntityReference parentRef, int stageIdx)
        {
            _logger.WriteLog($"Building query expression on {ENTITY_LOGICAL_NAME}");

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
                                ProcessStageMetadataFields.Index,
                                ConditionOperator.Equal,
                                stageIdx
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
                    $"Unable to resolve AssignedTeam for Process Stage with parent ID: {parentRef.Id}, stageIdx: {stageIdx}"
                );
            }
        }
    }
}