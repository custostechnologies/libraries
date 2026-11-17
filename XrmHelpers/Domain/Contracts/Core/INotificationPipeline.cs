using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface INotificationPipeline<TDto>
    {
        Guid BuildEmailNotification(
            Entity target, 
            OperationType type, 
            string to, 
            string? cc, 
            string fallbackName,
            IReadOnlyDictionary<string, string?>? customTags = null
        );
        List<Guid> BuildDigestEmailNotification(
            string targetEntity,
            OperationType type,
            ColumnSet columns,
            IReadOnlyDictionary<string, string?>? customTags = null
        );
    }
}