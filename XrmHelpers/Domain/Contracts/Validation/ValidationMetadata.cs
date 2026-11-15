namespace Custos.XrmHelpers.Domain.Contracts
{
    public class ValidationError : IValidationError
    {
        public ValidationError(string targetAttribute, string message)
        {
            TargetAttribute = targetAttribute ?? string.Empty;
            Message = message ?? string.Empty;
        }
        public string TargetAttribute { get; }
        public string Message { get; }
    }
    public class ValidationWarning : IValidationWarning
    {
        public ValidationWarning(string targetAttribute, string message)
        {
            TargetAttribute = targetAttribute ?? string.Empty;
            Message = message ?? string.Empty;
        }
        public string TargetAttribute { get; }
        public string Message { get; }
    }

    public class ValidationResult : IValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<IValidationError> Errors { get; } = new();
        public List<IValidationWarning> Warnings { get; } = new();
    }

    public class ValidationMetadata
    {
        public ValidationMetadata(string ruleSetName)
        {
            ValidationRuleSet = ruleSetName ?? "";
        }
        public string ValidationRuleSet { get; }
    }
}