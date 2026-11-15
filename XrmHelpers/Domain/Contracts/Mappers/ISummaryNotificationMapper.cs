using Custos.XrmHelpers.Infrastructure.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface ISummaryNotificationMapper
    {
        Guid CreateSummaryRecord<TDto>(TDto dto, List<SummaryField> fields);
    }
}