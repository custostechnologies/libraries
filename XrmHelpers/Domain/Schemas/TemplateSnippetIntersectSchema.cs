using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class TemplateSnippetIntersectSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet AllColumns = new ColumnSet(
            TemplateSnippetIntersectFields.InjectionOrder,
            TemplateSnippetIntersectFields.Name,
            TemplateSnippetIntersectFields.RelatedEmailTemplate,
            TemplateSnippetIntersectFields.RelatedHTMLSnippet,
            TemplateSnippetIntersectFields.TemplateSnippetIntersectId,
            TemplateSnippetIntersectFields.Status,
            TemplateSnippetIntersectFields.StatusReason
        );
    }
}