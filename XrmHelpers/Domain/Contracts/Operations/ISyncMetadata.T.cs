using custos.XrmHelpers.Domain.Enums;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface ISyncMetadata
    {
        public Guid? SourceId { get; set; }
        public string? SourceEntityLogicalName { get; set; }
        public int SourceVersion { get; set; }
        public Guid? TargetId { get; set; }
        public string? TargetEntityLogicalName { get; set; }
        public int TargetVersion { get; set; }
        public bool IsFullSync { get; set; }
        public int? CurrentHash { get; set; }
        public int? PreviousHash { get; set; }
        public bool? HasChanged { get; set; }
        public DateTime? LastSyncedOn { get; set; }
        public Guid CorrelationId { get; set; }
        public SyncStatus Status { get; set; }
    }
}