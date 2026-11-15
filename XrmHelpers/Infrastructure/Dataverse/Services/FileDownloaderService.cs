using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Services
{
    public class FileDownloaderService
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private string _fileAttributeName = string.Empty;
        private EntityReference? _target;
        public FileDownloaderService(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<FileDownloaderService>();
        }

        public byte[] Execute(
            EntityReference target,
            string fileAttributeName
        )
        {
            _target = target;
            _fileAttributeName = fileAttributeName;

            _logger.WriteLog(
                "Starting file download. Entity={0}, Id={1}, Attribute={2}",
                target.LogicalName,
                target.Id,
                fileAttributeName
            );

            try
            {
                var initRequest = new InitializeFileBlocksDownloadRequest
                {
                    Target = _target,
                    FileAttributeName = _fileAttributeName
                };

                _logger.WriteLog(
            "Sending InitializeFileBlocksDownloadRequest."
        );

                var initResponse = (InitializeFileBlocksDownloadResponse)_context.Runtime.UserService.Execute(initRequest);

                string fileContinuationToken = initResponse.FileContinuationToken;
                long fileSize = initResponse.FileSizeInBytes;

                _logger.WriteLog(
            "Download initialized. FileSize={0} bytes, TokenPresent={1}",
            fileSize,
            !string.IsNullOrWhiteSpace(fileContinuationToken)
        );

                List<byte> fileBytes = new((int)fileSize);

                long offset = 0;
                long blockSize = 4 * 1024 * 1024;

                if (fileSize < blockSize) blockSize = fileSize;

                _logger.WriteLog(
            "Using block size of {0} bytes.",
            blockSize
        );

                while (fileSize > 0)
                {
                    _logger.WriteLog(
                "Downloading block. Offset={0}, BlockLength={1}, Remaining={2}",
                offset,
                blockSize,
                fileSize
            );

                    var downloadBlockRequest = new DownloadBlockRequest
                    {
                        BlockLength = blockSize,
                        Offset = offset,
                        FileContinuationToken = fileContinuationToken
                    };

                    DownloadBlockResponse response = (DownloadBlockResponse)_context.Runtime.UserService.Execute(downloadBlockRequest);

                    _logger.WriteLog(
                "Block retrieved. BytesReceived={0}",
                response.Data?.Length ?? 0
            );

                    fileBytes.AddRange(response.Data ?? []);

                    fileSize -= (int)blockSize;
                    offset += blockSize;
                }

                _logger.WriteLog(
            "File download completed successfully. TotalBytes={0}",
            fileBytes.Count
        );

                return fileBytes.ToArray();
            }
            catch (Exception ex)
            {
                _logger.WriteLog(
                    ex,
                    $"File download failed. Entity={_target?.LogicalName}, Id={_target?.Id}, Attribute={_fileAttributeName}"
                );
                throw new InvalidPluginExecutionException($"Error saving file: ", ex);
            }
        }
    }
}