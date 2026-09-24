using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services
{
    public class FileDownloaderService
    {
        private IDataverseContext _context;
        private DataverseLogger _logger;
        private string _fileAttributeName = string.Empty;
        private EntityReference? _target;
        public FileDownloaderService(IDataverseContext context)
        {
            _context = context;
            _logger = new DataverseLogger(
                _context.Logger,
                nameof(FileDownloaderService)
            );
        }

        public byte[] Execute(
            EntityReference target,
            string fileAttributeName
        )
        {
            _target = target;
            _fileAttributeName = fileAttributeName;

            try
            {
                var initRequest = new InitializeFileBlocksDownloadRequest
                {
                    Target = _target,
                    FileAttributeName = _fileAttributeName
                };

                var initResponse = (InitializeFileBlocksDownloadResponse)_context.Service.Execute(initRequest);

                string fileContinuationToken = initResponse.FileContinuationToken;
                long fileSize = initResponse.FileSizeInBytes;

                List<byte> fileBytes = new((int)fileSize);

                long offset = 0;
                long blockSize = 4 * 1024 * 1024;

                if (fileSize < blockSize) blockSize = fileSize;

                while (fileSize > 0)
                {
                    var downloadBlockRequest = new DownloadBlockRequest
                    {
                        BlockLength = blockSize,
                        Offset = offset,
                        FileContinuationToken = fileContinuationToken
                    };

                    DownloadBlockResponse response = (DownloadBlockResponse)_context.Service.Execute(downloadBlockRequest);

                    fileBytes.AddRange(response.Data);

                    fileSize -= (int)blockSize;
                    offset += blockSize;
                }

                return fileBytes.ToArray();
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Upload failed: {ex.Message}");
                throw new InvalidPluginExecutionException($"Error saving file: {ex.Message}");
            }
        }
    }
}