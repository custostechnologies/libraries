using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationPipeline<TDto>
    {
        Entity BuildEmailNotification(Entity target);
        //Entity BuildTeamsNotification(Entity target);
    }
}