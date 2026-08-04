namespace spgsWebTools.XrmHelpers.Shared.Utilities
{
    public static string SafeString(string input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            return input.ToLowerInvariant().Trim().Replace(" ", "");
        } else
        {
            throw new Exception("Unable to normalise SafeString input.");
        }
    }
}