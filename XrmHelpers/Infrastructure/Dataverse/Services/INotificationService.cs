using Microsoft.Xrm.Sdk;

using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Infrastructure.Dataverse.Models;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Services
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