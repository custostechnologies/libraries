namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public enum LuFailureReason
    {
        RecordNotFound,
        MultipleMatches,
        InvalidKeyFormat,
        SecurityRestriction,
        DataverseQueryFailure,
        MissingLookupConfiguration,
        Unknown
    }
    public static class LuFailureReasonExtensions
    {
        public static LuFailureReason ToLuFailureReason(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return LuFailureReason.Unknown;
            return value switch
            {
                var r when r.Equals("recordnotfound", StringComparison.OrdinalIgnoreCase) => LuFailureReason.RecordNotFound,
                var r when r.Equals("multiplematches", StringComparison.OrdinalIgnoreCase) => LuFailureReason.MultipleMatches,
                var r when r.Equals("invalidkeyformat", StringComparison.OrdinalIgnoreCase) => LuFailureReason.InvalidKeyFormat,
                var r when r.Equals("securityrestriction", StringComparison.OrdinalIgnoreCase) => LuFailureReason.SecurityRestriction,
                var r when r.Equals("dataversequeryfailure", StringComparison.OrdinalIgnoreCase) => LuFailureReason.DataverseQueryFailure,
                var r when r.Equals("missinglookupconfiguration", StringComparison.OrdinalIgnoreCase) => LuFailureReason.MissingLookupConfiguration,
                _ => LuFailureReason.Unknown
            };
        }

        public static string ToString(this LuFailureReason reason)
        {
            return reason switch
            {
                LuFailureReason.RecordNotFound => "Record Not Found",
                LuFailureReason.MultipleMatches => "Multiple Matches",
                LuFailureReason.InvalidKeyFormat => "Invalid Key Format",
                LuFailureReason.SecurityRestriction => "Security Restriction",
                LuFailureReason.DataverseQueryFailure => "Dataverse Query Failure",
                LuFailureReason.MissingLookupConfiguration => "Missing Lookup Configuration",
                _ => "Unknown - Encountered unknown failure reason"
            };
        }
    }
}