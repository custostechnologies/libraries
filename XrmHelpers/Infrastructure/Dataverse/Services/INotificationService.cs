using Microsoft.Xrm.Sdk;

using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Models;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services
{
    public interface INotificationService<TDto>
    {
        void Notify(
            EntityReference ownerRef,
            OperationType operationType,
            Entity notifiableEntity
        );
        void Notify(
            User user,
            OperationType operationType,
            Entity notifiableEntity
        );
    }
}