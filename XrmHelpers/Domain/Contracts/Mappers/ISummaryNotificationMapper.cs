using spgsWebTools.XrmHelpers.Infrastructure.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ISummaryNotificationMapper
    {
        Guid CreateSummaryRecord<TDto>(TDto dto, List<SummaryField> fields);
    }
}