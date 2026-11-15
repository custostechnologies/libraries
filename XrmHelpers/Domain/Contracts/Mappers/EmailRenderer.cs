using System.Text;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class EmailRenderer : IEmailRenderer
    {
        public readonly IDataverseLogger _logger;
        public EmailRenderer(IDataverseLoggerFactory loggerFactory)
        {
            _logger = loggerFactory.Create<EmailRenderer>();
        }
        public string Render(
            NotificationTemplate template,
            IReadOnlyCollection<IInjectionTag> tags
        )
        {
            var sb = new StringBuilder(template.HtmlTemplate);

            foreach (var tag in tags)
            {
                if (tag.Value == null) continue;

                var logValue = tag.InjectionToken == "inTableHtmlString" || tag.InjectionToken == "inEmailContentHtml"
                    ? "HTML Omitted"
                    : tag.Value;
                    
                _logger.WriteLog($"Replacing {tag.InjectionToken} with {logValue}");
                var adjustedTag = string.Concat(
                    "{{",
                    tag.InjectionToken,
                    "}}"
                );

                sb.Replace(adjustedTag, tag.Value.ToString());
            }

            return sb.ToString();
        }
    }
}