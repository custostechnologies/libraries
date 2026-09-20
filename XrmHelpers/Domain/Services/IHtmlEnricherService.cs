using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Services
{
    public interface IHtmlEnricherService<TDto>
    {
        string Enrich(TDto dto, ThemeDto theme, List<TemplateSnippetIntersectDto> intersects);
    }
}