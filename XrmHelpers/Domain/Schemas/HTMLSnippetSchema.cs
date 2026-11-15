using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class HTMLSnippetSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet AllColumns = new ColumnSet(
            HTMLSnippetFields.Description,
            HTMLSnippetFields.HTMLFile,
            HTMLSnippetFields.InjectionTags,
            HTMLSnippetFields.Name,
            HTMLSnippetFields.Sequence,
            HTMLSnippetFields.HTMLSnippetId,
            HTMLSnippetFields.Status,
            HTMLSnippetFields.StatusReason
        );
    }
}