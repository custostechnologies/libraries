using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface INotifiableDto : IDto
    {
        public abstract Guid EntityId { get; set; }
        public abstract string TargetAppDisplayName { get; set; }
        public abstract string GenerateSummaryTable(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        );
        public abstract string LinkToRecord(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        );
        public OperationType ContextOperationType { get; set; }
        public string HelpMeLink { get; set; }
    }
}