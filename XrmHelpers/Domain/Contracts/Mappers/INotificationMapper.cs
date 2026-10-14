using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        (string html, NotificationRequestDto request) CreateHtmlObject(TDto dto);
        (string html, NotificationRequestDto request) CreateSummaryHtmlObject(List<TDto> dtos);
        (string html, NotificationRequestDto request) RenderHtmlObject(
            NotificationContext context, 
            OperationType type, 
            string recipientEmail, 
            string recipientName
        );
        Guid MapToNotificationRequest(NotificationRequestDto dto, string htmlContent);
    }
}