using System.Net;
using System.Text;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;
using Custos.XrmHelpers.Infrastructure.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class TagResolver : ITagResolver
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        private readonly EnvironmentVariableValues _environmentVariableQuery;
        private const string BANNER_ASSET_VAR_NAME = "mthomas_envVar_emailBannerAsset_url";
        private const string FOOTER_ASSET_VAR_NAME = "mthomas_envVar_emailFooterAsset_url";
        public TagResolver(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context,
            EnvironmentVariableValues environmentVariableQuery
        )
        {
            _context = context;
            _logger = loggerFactory.Create<TagResolver>();
            _environmentVariableQuery = environmentVariableQuery;
        }
        public List<IInjectionTag> BuildTags(
            NotificationContext context,
            NotificationTemplate template
        )
        {
            List<IInjectionTag> tags = new EmailInjectionTags().Tags;
            _logger.WriteLog($"Constructed tags list, tags.Count: {tags.Count}");

            InjectionTagResolverEngine resolverEngine = new InjectionTagResolverEngine();
            // populate tags from emailTemplate
                if (template.Theme != null)
                {
                    resolverEngine.PopulateDataverseTags(
                        template.Theme,
                        tags
                    );
                }

            // populate tags from emailTemplate
            if (template.EmailTemplate != null)
            {
                resolverEngine.PopulateDataverseTags(
                    template.EmailTemplate,
                    tags
                );
            }

            resolverEngine.PopulateDataverseTags(context.Dto, tags);

            resolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inButtonTargetUrl", 
                context.ButtonUrl ?? ""
            );

            resolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inButtonText", 
                context.ButtonText
            );

            resolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inHelpMeLinkUrl", 
                context.Dto != null ? context.Dto.HelpMeLink : ""
            );

            resolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inReceiverFirstName", 
                context.RecipientName
            );

            var footerAssetUrl = _environmentVariableQuery.GetValue<string>(FOOTER_ASSET_VAR_NAME)?.Value ?? "";

            resolverEngine.PopulateTag(
                context.Dto, tags, "inFooterImageUrl",
                footerAssetUrl
            );

            var bannerAssetUrl = _environmentVariableQuery.GetValue<string>(BANNER_ASSET_VAR_NAME)?.Value ?? "";

            resolverEngine.PopulateTag(
                context.Dto, tags, "inHeaderLogoImageUrl",
                bannerAssetUrl
            );

            resolverEngine.PopulateTag(
                context.Dto, tags, "inCorrelationId",
                _context.Runtime.ExecutionContext.CorrelationId.ToString()
            );

            resolverEngine.PopulateTag(
                context.Dto,
                tags,
                "inTableHtmlString",
                context.Table
            );

            var contentCardStringBuilder = new StringBuilder(template.ContentCardHtml ?? "");
            var inlineTableStringBuilder = new StringBuilder(template.TableHtml ?? "");
            var singleButtonStringBuilder = new StringBuilder(template.ButtonHtml ?? "");

            foreach (var tag in tags)
            {
                if (tag.Value == null) continue;

                var adjustedTag = string.Concat(
                    "{{",
                    tag.InjectionToken,
                    "}}"
                );

                string safeValue;

                switch (tag.InjectionToken)
                {
                    case "inTableHtmlString":
                        safeValue = tag.Value?.ToString() ?? "";
                        break;

                    case "inEmailContentHtml":
                        safeValue = tag.Value?.ToString() ?? "";
                        break;

                    default:
                        safeValue = WebUtility.HtmlEncode(tag.Value?.ToString() ?? "");
                        break;
                }

                contentCardStringBuilder.Replace(adjustedTag, safeValue);
                inlineTableStringBuilder.Replace(adjustedTag, safeValue);
                singleButtonStringBuilder.Replace(adjustedTag, safeValue);
            }

            var emailContentHtml = $@"
                {contentCardStringBuilder}
                {inlineTableStringBuilder}
                {singleButtonStringBuilder}
            ";

            resolverEngine.PopulateTag(
                context.Dto, tags, "inEmailContentHtml",
                emailContentHtml
            );

            resolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inEnvId", 
                _context.Runtime.ExecutionContext.OrganizationId.ToString()
            );

            return tags;
        }
    }
}