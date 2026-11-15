namespace Custos.XrmHelpers.Constants
{
    public static class EmailTemplateFields
    {
        // Identity
        public const string EmailTemplateId = "mthomas_emailtemplateid";

        // Core Fields
        public const string Name = "mthomas_name";
        public const string Subject = "mthomas_subject";
        public const string EmailTemplateKey = "mthomas_emailtemplatekey";
        public const string Importance = "mthomas_importance";

        // Email Content
        public const string SendFrom = "mthomas_sendfrom";
        public const string CC = "mthomas_cc";
        public const string ContentBodyText = "mthomas_contentbodytext";
        public const string ContentCardHeader = "mthomas_contentcardheader";
        public const string HeaderTitle = "mthomas_headertitle";
        public const string HeaderSubtitle = "mthomas_headersubtitle";

        // Templates & Files
        public const string HtmlTemplate = "mthomas_htmltemplate";
        public const string FetchXmlQuery = "mthomas_fetchxmlquery";

        // Relationships
        public const string ExportTemplate = "mthomas_exporttemplate";
        public const string Theme = "mthomas_theme";
        public const string RelatedCloudFlow = "mthomas_relatedcloudflow";

        // Status
        public const string StateCode = "statecode";
        public const string StatusCode = "statuscode";
    }
}