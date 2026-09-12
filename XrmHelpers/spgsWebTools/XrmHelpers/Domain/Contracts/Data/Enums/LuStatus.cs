namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public enum LuStatus
    {
        NotAttempted,
        Succeeded,
        SucceededWithWarnings,
        SucceededWithErrors,
        Failed,
        Unknown
    }
    public static class LuStatusExtensions
    {
        public static LuStatus ToLuStatusReason(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return LuStatus.Unknown;
            return value switch
            {
                var r when r.Equals("notattempted", StringComparison.OrdinalIgnoreCase) => LuStatus.NotAttempted,
                var r when r.Equals("succeeded", StringComparison.OrdinalIgnoreCase) => LuStatus.Succeeded,
                var r when r.Equals("succeededwithwarning", StringComparison.OrdinalIgnoreCase) => LuStatus.SucceededWithWarnings,
                var r when r.Equals("succeededwitherrors", StringComparison.OrdinalIgnoreCase) => LuStatus.SucceededWithErrors,
                var r when r.Equals("failed", StringComparison.OrdinalIgnoreCase) => LuStatus.Failed,
                _ => LuStatus.Unknown
            };
        }

        public static string ToString(this LuStatus reason)
        {
            return reason switch
            {
                LuStatus.NotAttempted => "Not Attempted",
                LuStatus.Succeeded => "Succeeded",
                LuStatus.SucceededWithWarnings => "Succeeded with Warnings",
                LuStatus.SucceededWithErrors => "Succeeded with Errors",
                LuStatus.Failed => "Failed",
                _ => "Unknown - Encountered unknown failure reason"
            };
        }
    }
}