using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class EmailTemplateSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet AllColumns = new ColumnSet(
            EmailTemplateFields.EmailTemplateId,
            EmailTemplateFields.Name,
            EmailTemplateFields.Subject,
            EmailTemplateFields.EmailTemplateKey,
            EmailTemplateFields.Importance,
            EmailTemplateFields.SendFrom,
            EmailTemplateFields.CC,
            EmailTemplateFields.ContentBodyText,
            EmailTemplateFields.ContentCardHeader,
            EmailTemplateFields.HeaderTitle,
            EmailTemplateFields.HeaderSubtitle,
            EmailTemplateFields.HtmlTemplate,
            EmailTemplateFields.FetchXmlQuery,
            EmailTemplateFields.ExportTemplate,
            EmailTemplateFields.Theme,
            EmailTemplateFields.StateCode,
            EmailTemplateFields.StatusCode
        );
    }
}