using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Domain.Services
{
    public interface IHtmlEnricherService<TDto>
    {
        string Enrich(TDto dto, ThemeDto theme, List<TemplateSnippetIntersectDto> intersects);
    }
}