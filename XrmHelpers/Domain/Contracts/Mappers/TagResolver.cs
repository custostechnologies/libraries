using System.Text;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;
using Custos.XrmHelpers.Infrastructure.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class TagResolver : ITagResolver
    {
        private readonly IDataverseLogger _logger;
        private readonly IDataverseContext _context;
        public TagResolver(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context
        )
        {
            _context = context;
            _logger = loggerFactory.Create<TagResolver>();
        }
        public List<IInjectionTag> BuildTags(
            NotificationContext context,
            NotificationTemplate template
        )
        {
            List<IInjectionTag> tags = new EmailInjectionTags().Tags;
            _logger.WriteLog($"Constructed tags list, tags.Count: {tags.Count}");

            InjectionTagResolverEngine _resolverEngine = new InjectionTagResolverEngine();
            // populate tags from emailTemplate
                if (template.Theme != null)
                {
                    _resolverEngine.PopulateDataverseTags(
                        template.Theme,
                        tags
                    );
                }

            InjectionTagResolverEngine _tagResolverEngine = new InjectionTagResolverEngine();

            // populate tags from emailTemplate
            if (template.EmailTemplate != null)
            {
                _tagResolverEngine.PopulateDataverseTags(
                    template.EmailTemplate,
                    tags
                );
            }

            _tagResolverEngine.PopulateDataverseTags(context.Dto, tags);

            _tagResolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inButtonTargetUrl", 
                context.ButtonUrl ?? "https://www.google.com"
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inButtonText", 
                context.ButtonText
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inHelpMeLinkUrl", 
                context.Dto != null ? context.Dto.HelpMeLink : ""
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inReceiverFirstName", 
                context.RecipientName
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, tags, "inFooterImageUrl",
                "https://spgswebresources.blob.core.windows.net/assets/crest_main_20260217.png"
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, tags, "inHeaderLogoImageUrl",
                "https://spgswebresources.blob.core.windows.net/assets/Custos_logo_noName_72ppi.png"
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, tags, "inCorrelationId",
                _context.Runtime.ExecutionContext.CorrelationId.ToString()
            );

            _tagResolverEngine.PopulateTag(
                context.Dto,
                tags,
                "inTableHtmlString",
                context.Table
            );

            var contentCardStringBuilder = new StringBuilder(template.ContentCardHtml);
            var inlineTableStringBuilder = new StringBuilder(template.TableHtml);
            var singleButtonStringBuilder = new StringBuilder(template.ButtonHtml);

            foreach (var tag in tags)
            {
                if (tag.Value == null) continue;

                var adjustedTag = string.Concat(
                    "{{",
                    tag.InjectionToken,
                    "}}"
                );

                contentCardStringBuilder.Replace(adjustedTag, tag.Value.ToString());
                inlineTableStringBuilder.Replace(adjustedTag, tag.Value.ToString());
                singleButtonStringBuilder.Replace(adjustedTag, tag.Value.ToString());
            }

            var emailContentHtml = $@"
                {contentCardStringBuilder}
                {inlineTableStringBuilder}
                {singleButtonStringBuilder}
            ";

            _tagResolverEngine.PopulateTag(
                context.Dto, tags, "inEmailContentHtml",
                emailContentHtml
            );

            _tagResolverEngine.PopulateTag(
                context.Dto, 
                tags, 
                "inEnvId", 
                _context.Runtime.ExecutionContext.OrganizationId.ToString()
            );

            return tags;
        }
    }
}