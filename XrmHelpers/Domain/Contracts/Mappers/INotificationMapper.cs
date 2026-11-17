using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        Guid CreateNotification(
            TDto dto,
            OperationType type, 
            string recipientEmail, 
            string recipientName,
            string cc,
            IReadOnlyDictionary<string, string?>? customTags = null
        );
        Guid CreateSummaryNotification(
            IReadOnlyCollection<TDto> dtos,
            OperationType type, 
            string recipientEmail, 
            string recipientName,
            string cc,
            string? metricsTableHtml,
            IReadOnlyDictionary<string, string?>? customTags = null
        );
        (
            string html, 
            NotificationRequestDto request
        ) Render(
            NotificationContext context, 
            OperationType type, 
            string recipientEmail, 
            string recipientName,
            IReadOnlyDictionary<string, string?>? customTags = null
        );
    }
}