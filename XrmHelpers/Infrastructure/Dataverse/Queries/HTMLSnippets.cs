using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class HTMLSnippets
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.HTMLSnippet;
        public HTMLSnippets(IDataverseContext context)
        {
            _context = context;
            _logger = new DataverseLogger(
                _context.Logger,
                nameof(HTMLSnippets)
            );
        }
        public Entity? GetSnippetByRef(EntityReference entityRef)
        {
            _logger.WriteLog($"Building query expression on {ENTITY_LOGICAL_NAME}");

            var query = new QueryExpression(ENTITY_LOGICAL_NAME)
            {
                TopCount = 1,
                ColumnSet = HTMLSnippetSchema.AllColumns,
                Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                HTMLSnippetFields.HTMLSnippetId,
                                ConditionOperator.Equal,
                                entityRef.Id
                            )
                        }
                    }
            };

            _logger.WriteLog($"Executing query expression on {ENTITY_LOGICAL_NAME}");

            var results = _context.Service.RetrieveMultiple(query);

            _logger.WriteLog("Finished executing query expression, interpreting results...");

            if (results.Entities.Count > 0)
            {
                _logger.WriteLog($"{results.Entities.Count} entities returned. Returning results.Entities[0].");
                return results.Entities[0];
            }
            else
            {
                _logger.WriteLog("No matching entities were found");
                return null;
            }
        }
    }
}