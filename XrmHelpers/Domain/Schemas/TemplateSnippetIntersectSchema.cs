using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
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
            TemplateSnippetIntersectFields.RelatedHTMLSnippet,
            TemplateSnippetIntersectFields.TemplateSnippetIntersectId,
            TemplateSnippetIntersectFields.Status,
            TemplateSnippetIntersectFields.StatusReason
        );
    }
}