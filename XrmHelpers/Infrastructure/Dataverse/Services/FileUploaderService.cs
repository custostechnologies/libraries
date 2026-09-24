using System;
using System.Text;
using Microsoft.Xrm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using System.Collections.Generic;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services
{
    public class FileUploaderService
    {
        private IDataverseContext _context;
        private DataverseLogger _logger;
        private string _exportContent = string.Empty;
        private string _fileAttributeName = string.Empty;
        private string _exportFileName = string.Empty;
        private EntityReference? _target;
        public FileUploaderService(
            IDataverseContext context
        )
        {
            _context = context;

            _logger = new DataverseLogger(
                _context.Logger,
                nameof(FileUploaderService)
            );
        }

        public void Execute(
            string exportContent,
            EntityReference target,
            string fileAttributeName,
            string exportFileName
        )
        {
            _exportContent = exportContent;
            _target = target;
            _fileAttributeName = fileAttributeName;
            _exportFileName = exportFileName;

            try
            {
                byte[] fileBytes = Encoding.UTF8.GetBytes(_exportContent);

                var initRequest = new InitializeFileBlocksUploadRequest
                {
                    Target = _target,
                    FileAttributeName = _fileAttributeName,
                    FileName = _exportFileName
                };

                var initResponse = (InitializeFileBlocksUploadResponse)_context.Service.Execute(initRequest);
                string fileContinuationToken = initResponse.FileContinuationToken;

                int offset = 0;
                int blockSize = 4 * 1024 * 1024;
                int totalSize = fileBytes.Length;
                int blockNumber = 1;

                var blockIds = new List<string>();

                while (offset < totalSize)
                {
                    int currentBlockSize = Math.Min(blockSize, totalSize - offset);
                    byte[] blockData = new byte[currentBlockSize];
                    Array.Copy(fileBytes, offset, blockData, 0, currentBlockSize);

                    string encodedBlockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(blockNumber.ToString("D6")));
                    blockIds.Add(encodedBlockId);

                    var uploadBlockRequest = new UploadBlockRequest
                    {
                        FileContinuationToken = fileContinuationToken,
                        BlockData = blockData,
                        BlockId = encodedBlockId
                    };

                    _context.Service.Execute(uploadBlockRequest);

                    offset += currentBlockSize;
                    blockNumber++;
                }

                // 4. Commit the file upload
                var commitRequest = new CommitFileBlocksUploadRequest
                {
                    FileContinuationToken = fileContinuationToken,
                    BlockList = blockIds.ToArray(),
                    FileName = _exportFileName,
                    MimeType = "text/plain"
                };

                _context.Service.Execute(commitRequest);
                _logger.WriteLog("File successfully written to Dataverse column.");
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Upload failed: {ex.Message}");
                throw new InvalidPluginExecutionException($"Error saving file: {ex.Message}");
            }
        }
    }
}