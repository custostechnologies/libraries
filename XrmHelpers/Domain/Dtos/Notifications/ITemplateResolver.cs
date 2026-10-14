using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationTemplate
    {
        public EmailTemplateDto? EmailTemplate { get; init; }

        public ThemeDto? Theme { get; init; }

        public string HtmlTemplate { get; init; } = string.Empty;

        public string ContentCardHtml { get; init; } = string.Empty;

        public string TableHtml { get; init; } = string.Empty;

        public string ButtonHtml { get; init; } = string.Empty;
    }
    public interface ITemplateResolver
    {
        NotificationTemplate Resolve(Type dtoType);
    }
}