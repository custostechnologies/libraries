using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse.Models;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Services
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