using System;
using Microsoft.Xrm.Sdk;
using System.Linq;
using System.Security.Cryptography;

namespace spgsWebTools.XrmHelpers.Shared.Hashing
{
    public class ImageHashService
    {
        private ITracingService _tracer;
        public ImageHashService(ITracingService tracer)
        {
            _tracer = tracer;
        }
        public string Calculate(byte[] image)
        {
            _logger.WriteLog("In ImageHashService.Calculate, calculating new SHA256 hash.");

            var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(image);
            string hexHash = string.Concat(
                hash.Select(bit => bit.ToString("X2"))
            );

            _logger.WriteLog("Returning to caller with hash: {0}.", hexHash);
            return hexHash;
        }

        public bool Compare(string existing, string incoming)
        {
            _logger.WriteLog("In ImageHashService.Compare, performing hash comparison between existing: {0} and incoming: {1}", existing, incoming);

            return existing == incoming;
        }
    }
}