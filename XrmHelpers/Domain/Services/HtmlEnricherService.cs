using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Shared.Hashing;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using System;
using System.Linq;
using System.Reflection;
using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Domain.Services
{
    public class HtmlEnricherService<TDto> : IHtmlEnricherService<TDto>
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private ThemeDto? _theme;
        private List<TemplateSnippetIntersectDto>? _intersects;
        public HtmlEnricherService(
            IDataverseContext context
        )
        {
            _context = context;

            _logger = _context.GetLogger<HtmlEnricherService<TDto>>();
        }
        public string Enrich(TDto dto, ThemeDto theme, List<TemplateSnippetIntersectDto> intersects)
        {
            string emailBody = string.Empty;
            _theme = theme;
            _intersects = intersects;

            foreach (TemplateSnippetIntersectDto intersect in intersects)
            {
                string[] injectionTags = [];
                var order = intersect.InjectionOrder;

                if (intersect.InjectionTags != null && !string.IsNullOrWhiteSpace(intersect.InjectionTags))
                {
                    var stripped = intersect.InjectionTags
                        .Replace("{{", "")
                        .Replace("}}", "");

                    injectionTags = intersect.InjectionTags.Split(';');
                }
            }

            return emailBody;
        }
    }
}