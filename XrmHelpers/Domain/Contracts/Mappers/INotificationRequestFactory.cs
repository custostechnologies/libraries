using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Domain.Contracts
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