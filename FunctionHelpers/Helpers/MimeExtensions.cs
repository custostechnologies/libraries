namespace Custos.FunctionHelpers.Helpers
{
    public static class MimeExtensions
    {
        public static string FindType(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length < 4) return "application/octet-stream";

            return bytes switch
            {
                [0x89, 0x50, 0x4E, 0x47, ..] => "image/png",
                [0xFF, 0xD8, 0xFF, ..] => "image/jpeg",
                [0x47, 0x49, 0x46, 0x38, ..] => "image/gif",
                [0x25, 0x50, 0x44, 0x46, ..] => "application/pdf",
                [0x50, 0x4B, 0x03, 0x04, ..] => "application/zip",
                _ => "application/octet-stream"
            };
        }
    }
}