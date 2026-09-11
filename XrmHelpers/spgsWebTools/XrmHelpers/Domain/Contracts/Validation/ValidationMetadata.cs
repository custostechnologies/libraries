namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class ValidationMetadata(string ruleSetName)
    {
        ValidationRuleSet = ruleSetName ?? string.Empty;
    }
    public class ValidationError(string targetAttribute, string message)
    {
        public string TargetAttribute { get; } = targetAttribute ?? string.Empty;
        public string Message { get; } = message ?? string.Empty;
    }
    public class ValidationWarning(string targetAttribute, string message)
    {
        public string TargetAttribute { get; } = targetAttribute ?? string.Empty;
        public string Message { get; } = message ?? string.Empty;
    }
    public class ValidationResult(List<ValidationError> errors, List<ValidationWarning> warnings)
    {
        public bool IsValid => Errors.Count == 0;
        public List<ValidationError> Errors { get; } = [];
        public List<ValidationWarning> Warnings { get; } = [];
    }
}