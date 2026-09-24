using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Schemas;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class TemplateSnippetIntersects
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.TemplateSnippetIntersect;
        public TemplateSnippetIntersects(IDataverseContext context)
        {
            _context = context;
            _logger = new DataverseLogger(
                _context.Logger,
                nameof(Themes)
            );
        }
        public EntityCollection GetIntersectsByTemplateRef(EntityReference entityRef)
        {
            try
            {
                _logger.WriteLog($"Building query expression on {ENTITY_LOGICAL_NAME}");

                var query = new QueryExpression(ENTITY_LOGICAL_NAME)
                {
                    ColumnSet = TemplateSnippetIntersectSchema.AllColumns,
                    Criteria =
                    {
                        Conditions =
                        {
                            new ConditionExpression(
                                TemplateSnippetIntersectFields.RelatedEmailTemplate,
                                ConditionOperator.Equal,
                                entityRef.Id
                            )
                        }
                    }
                };

                _logger.WriteLog($"Adding LinkEntity...");

                LinkEntity snippetLink = query.AddLink(
                    linkToEntityName: EntityLogicalNames.HTMLSnippet,
                    linkFromAttributeName: TemplateSnippetIntersectFields.RelatedHTMLSnippet,
                    linkToAttributeName: HTMLSnippetFields.HTMLSnippetId,
                    joinOperator: JoinOperator.Inner
                );

                snippetLink.Columns.AddColumns(
                    HTMLSnippetFields.Description,
                    HTMLSnippetFields.InjectionTags,
                    HTMLSnippetFields.Sequence
                );

                snippetLink.EntityAlias = "sn";

                _logger.WriteLog($"Executing query expression on {ENTITY_LOGICAL_NAME}");

                var results = _context.Service.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression, interpreting results...");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"{results.Entities.Count} entities returned. Returning ALL results.Entities.");
                    return results;
                }
                else
                {
                    _logger.WriteLog("No matching entities were found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: {ex}");
                throw;
            }
        }
    }
}