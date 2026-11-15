
using System;
using custos.FunctionHelpers.Types;

namespace custos.FunctionHelpers.Implementations
{
    public class SharePointFileMetadata : ISharePointFileMetadata
    {
        public Guid AttachmentId { get; set; }
        public Guid RegardingRecordId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string AttachmentType { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SharePointFileId { get; set; } = string.Empty;
        public string SharePointItemId { get; set; } = string.Empty;
        public string SharePointDriveId { get; set; } = string.Empty;
        public string SharePointSiteId { get; set; } = string.Empty;
        public string SharePointUrl { get; set; } = string.Empty;
        public Guid UploadedByUserId { get; set; }
        public Guid UploadedByAzureId { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}