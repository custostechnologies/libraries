using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationPipeline<TDto>
    {
        Guid BuildEmailNotification(
            Entity target, 
            OperationType type, 
            string to, 
            string? cc, 
            string fallbackName
        );
        List<Guid> BuildDigestEmailNotification(
            string targetEntity,
            OperationType type,
            ColumnSet columns
        );
    }
}