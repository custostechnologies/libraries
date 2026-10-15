using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationRequestFactory
    {
        NotificationRequestDto Create(
            EmailTemplateDto template,
            Guid entityId,
            string recipientEmail,
            string cc
        );
    }
}