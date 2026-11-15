using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class EmailInjectionTags
    {
        public List<IInjectionTag> Tags = new List<IInjectionTag>
        {
            new DataverseInjectionTag
            {
                InjectionToken = "inContentTextBody",
                EntityLogicalName = EntityLogicalNames.EmailTemplate,
                AttributeLogicalName = EmailTemplateFields.ContentBodyText
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inContentHeader",
                EntityLogicalName = EntityLogicalNames.EmailTemplate,
                AttributeLogicalName = EmailTemplateFields.ContentCardHeader
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inHighlightHex",
                EntityLogicalName = EntityLogicalNames.Theme,
                AttributeLogicalName = ThemeFields.Highlight
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inPrimaryHex",
                EntityLogicalName = EntityLogicalNames.Theme,
                AttributeLogicalName = ThemeFields.Primary
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inSecondaryHex",
                EntityLogicalName = EntityLogicalNames.Theme,
                AttributeLogicalName = ThemeFields.Secondary
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inAlternateHex",
                EntityLogicalName = EntityLogicalNames.Theme,
                AttributeLogicalName = ThemeFields.Alternate
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inHeaderTitle",
                EntityLogicalName = EntityLogicalNames.EmailTemplate,
                AttributeLogicalName = EmailTemplateFields.HeaderTitle
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inHeaderSubtitle",
                EntityLogicalName = EntityLogicalNames.EmailTemplate,
                AttributeLogicalName = EmailTemplateFields.HeaderSubtitle
            },

            new DataverseInjectionTag
            {
                InjectionToken = "inWorkersCompClaimNumber",
                EntityLogicalName = EntityLogicalNames.WorkersCompensationClaim,
                AttributeLogicalName = WorkersCompensationClaimFields.ClaimNumber
            },

            new ContextInjectionTag
            {
                InjectionToken = "inReceiverFirstName",
                ContextKey = "ReceiverFirstName"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inSummaryType",
                ContextKey = "inSummaryType"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inAdditionalContent",
                ContextKey = "inAdditionalContent"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inTableHtmlString",
                ContextKey = "inTableHtmlString"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inButtonBorderSizeInt",
                ContextKey = "inButtonBorderSizeInt",
                Value = "5"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inButtonText",
                ContextKey = "inButtonText"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inEmailContentHtml",
                ContextKey = "inEmailContentHtml"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inHtmlSpace",
                ContextKey = "inHtmlSpace"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inEnvId",
                ContextKey = "inEnvId"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inCorrelationId",
                ContextKey = "inCorrelationId"
            },

            new ContextInjectionTag
            {
                InjectionToken = "inFlowId",
                ContextKey = "inFlowId"
            },

            new AssetSourceInjectionTag
            {
                InjectionToken = "inHelpMeLinkUrl",
                Type = InjectionAssetType.Url,
                ContextKey = "inHelpMeLinkUrl"
            },

            new AssetSourceInjectionTag
            {
                InjectionToken = "inHeaderLogoImageUrl",
                Type = InjectionAssetType.Url,
                ContextKey = "inHeaderLogoImageUrl"
            },

            new AssetSourceInjectionTag
            {
                InjectionToken = "inFooterImageUrl",
                Type = InjectionAssetType.Url,
                ContextKey = "inFooterLogoImageUrl"
            },

            new AssetSourceInjectionTag
            {
                InjectionToken = "inButtonTargetUrl",
                Type = InjectionAssetType.Url,
                ContextKey = "inButtonTargetUrl"
            }
        };
    }
}