using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationPipeline<TDto>
    {
        Guid BuildEmailNotification(Entity target, string to, string? cc);
        //Entity BuildTeamsNotification(Entity target);
    }
}