using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
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