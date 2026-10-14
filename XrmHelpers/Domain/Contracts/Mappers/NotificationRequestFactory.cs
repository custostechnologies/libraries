using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationRequestFactory : INotificationRequestFactory
    {
        public NotificationRequestDto Create(
            EmailTemplateDto template,
            Guid entityId,
            string recipientEmail
        )
        {
            return new NotificationRequestDto()
            {
                RequestTitle = template.Name,
                EmailTemplate = new EntityReference(
                    EntityLogicalNames.EmailTemplate,
                    template.EmailTemplateId ?? Guid.Empty
                ),
                RegardingRecordId = entityId,
                CCString = template.CC,
                Importance = 0,
                Subject = template.Subject,
                SendFrom = template.SendFrom,
                NotificationType = Enums.NotificationType.Email,
                RecipientString = recipientEmail
            };
        }
    }
}