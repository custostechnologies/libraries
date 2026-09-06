using System;
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
            _tracer.Trace("In ImageHashService.Calculate, calculating new SHA256 hash.");

            var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(image);
            string hexHash = Convert.ToHexString(hash);

            _tracer.Trace("Returning to caller with hash: {0}.", hexHash);
            return hexHash;
        }

        public bool Compare(string existing, string incoming)
        {
            _tracer.Trace("In ImageHashService.Compare, performing hash comparison between existing: {0} and incoming: {1}", existing, incoming);

            return existing == incoming;
        }
    }
}