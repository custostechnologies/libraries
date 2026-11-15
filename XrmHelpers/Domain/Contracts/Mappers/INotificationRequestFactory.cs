using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Domain.Contracts
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