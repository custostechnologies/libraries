using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationPipeline<TDto>
    {
        Guid BuildEmailNotification(Entity target, OperationType type, string to, string? cc, string fallbackName);
        //Entity BuildTeamsNotification(Entity target);
    }
}