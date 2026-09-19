using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    interface ISyncOperation
    {
        public SyncOperations? Operation { get; set; }
        public string Title
        { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public SyncOperationStatus Status { get; set; }
    }
}