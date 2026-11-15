using System;

namespace custos.FunctionHelpers.Types
{
    interface ISharePointFileMetadata
    {
        Guid AttachmentId { get; set; }
        Guid RegardingRecordId { get; set; }
        string FileName { get; set; }
        string OriginalFileName { get; set; }
        string FileExtension { get; set; }
        string ContentType { get; set; }
        long FileSizeBytes { get; set; }
        string AttachmentType { get; set; }
        string? Description { get; set; }
        string SharePointFileId { get; set; }
        string SharePointItemId { get; set; }
        string SharePointDriveId { get; set; }
        string SharePointSiteId { get; set; }
        string SharePointUrl { get; set; }
        Guid UploadedByUserId { get; set; }
        Guid UploadedByAzureId { get; set; }
        DateTime UploadedOn { get; set; }
    }
}