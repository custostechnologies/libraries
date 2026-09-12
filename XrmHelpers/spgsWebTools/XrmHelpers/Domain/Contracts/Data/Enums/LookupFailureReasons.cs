namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public static class LookupFailureReasons
    {
        public const string RecordNotFound = "Record Not Found";
        public const string MultipleMatchesFound = "Multiple Matched Found";
        public const string InvalidKeyFormat = "Invalid Key Format";
        public const string SecurityRestriction = "Security Restriction";
        public const string DataverseQueryFailure = "Dataverse Query Failure";
        public const string MissingLookupConfiguration = "Missing Lookup Configuration";
    }
}