using System;
using System.Text.Json;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Schemas;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class TemplateSnippetIntersects
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly string ENTITY_LOGICAL_NAME = EntityLogicalNames.TemplateSnippetIntersect;
        public TemplateSnippetIntersects(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<TemplateSnippetIntersects>();
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

                var results = _context.Runtime.UserService.RetrieveMultiple(query);

                _logger.WriteLog("Finished executing query expression, interpreting results...");

                if (results.Entities.Count > 0)
                {
                    _logger.WriteLog($"{results.Entities.Count} entities returned. Returning ALL results.Entities.");
                    return results;
                }
                else
                {
                    _logger.WriteLog("No matching entities were found");
                    return new EntityCollection();
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: ", ex);
                throw;
            }
        }
    }
}