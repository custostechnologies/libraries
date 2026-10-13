using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        (string, NotificationRequestDto) CreateHtmlObject(TDto dto, OperationType type, string recipientEmail, string recipientName);
        Guid MapToNotificationRequest(NotificationRequestDto dto, string htmlContent);
    }
}