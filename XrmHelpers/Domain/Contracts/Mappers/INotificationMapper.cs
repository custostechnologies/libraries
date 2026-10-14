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
            string recipientName
        );
        Guid CreateSummaryNotification(
            List<TDto> dtos,
            OperationType type, 
            string recipientEmail, 
            string recipientName
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