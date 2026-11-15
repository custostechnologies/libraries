using custos.XrmHelpers.Infrastructure.Services;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface ISummaryNotificationMapper
    {
        Guid CreateSummaryRecord<TDto>(TDto dto, List<SummaryField> fields);
    }
}