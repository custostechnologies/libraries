using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotifiableDto : IDto
    {
        public abstract Guid EntityId { get; set; }
        public abstract string TargetAppDisplayName { get; set; }
        public abstract string GenerateSummaryTable(
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        );
        public abstract string LinkToRecord(
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        );
        public OperationType ContextOperationType { get; set; }
    }
}