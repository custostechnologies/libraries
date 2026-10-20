using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        Guid CreateNotification(
            TDto dto,
            OperationType type, 
            string recipientEmail, 
            string recipientName,
            string cc
        );
        Guid CreateSummaryNotification(
            IReadOnlyCollection<TDto> dtos,
            OperationType type, 
            string recipientEmail, 
            string recipientName,
            string cc,
            string? metricsTableHtml
        );
        (
            string html, 
            NotificationRequestDto request
        ) Render(
            NotificationContext context, 
            OperationType type, 
            string recipientEmail, 
            string recipientName
        );
    }
}