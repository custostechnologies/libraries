using System.Windows.Forms;

namespace spgsWebTools.XrmHelpers.Shared.Utilities
{
    public class SafeString
    {
        public static string Normalise(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.ToLowerInvariant().Trim().Replace(" ", "");
            }
            else
            {
                throw new Exception("Unable to normalise SafeString input.");
            }
        }
    }
}