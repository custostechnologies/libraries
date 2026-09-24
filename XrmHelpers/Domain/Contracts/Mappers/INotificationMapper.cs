using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        (string, NotificationRequestDto) CreateHtmlObject(TDto dto);
        Guid MapToNotificationRequest(NotificationRequestDto dto, string htmlContent);
    }
}