using custos.XrmHelpers.Domain.Contracts;
using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Domain.Services
{
    public interface IHtmlEnricherService<TDto>
    {
        string Enrich(TDto dto, ThemeDto theme, List<TemplateSnippetIntersectDto> intersects);
    }
}