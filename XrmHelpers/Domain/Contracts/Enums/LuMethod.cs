namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public enum LuMethod
    {
        PrimaryKey,
        AlternateKey,
        CompositeKey,
        NoKey,
        Unknown
    }
    public static class LuMethodExtensions
    {
        public static LuMethod ToLuMethodReason(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return LuMethod.Unknown;
            return value switch
            {
                var r when r.Equals("primarykey", StringComparison.OrdinalIgnoreCase) => LuMethod.PrimaryKey,
                var r when r.Equals("alternatekey", StringComparison.OrdinalIgnoreCase) => LuMethod.AlternateKey,
                var r when r.Equals("compositekey", StringComparison.OrdinalIgnoreCase) => LuMethod.CompositeKey,
                var r when r.Equals("nokey", StringComparison.OrdinalIgnoreCase) => LuMethod.NoKey,
                _ => LuMethod.Unknown
            };
        }

        public static string ToString(this LuMethod reason)
        {
            return reason switch
            {
                LuMethod.PrimaryKey => "Primary Key - Records matched using the record's primary key (GUID)",
                LuMethod.AlternateKey => "Alternate Key - Records matched using an attribute designated as an alternate key.",
                LuMethod.CompositeKey => "Composite Key - Records matched using a selection of attributes designated as a composite key.",
                LuMethod.NoKey => "No Key - Records matched using an attribute that is not a key.",
                _ => "Unknown - Encountered unknown failure reason"
            };
        }
    }
}