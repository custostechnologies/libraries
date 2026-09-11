namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class ValidationMetadata
    {
        public ValidationError(string targetAttribute, string message)
        {
            TargetAttribute = targetAttribute ?? string.Empty;
            Message = message ?? string.Empty;
        }
        public ValidationWarning(string targetAttribute, string message)
        {
            TargetAttribute = targetAttribute ?? string.Empty;
            Message = message ?? string.Empty;
        }
        public ValidationResult(List<ValidationError> errors, List<ValidationWarning> warnings)
        {
            IsValid => Errors.Count == 0;
            Errors = [];
    public List<ValidationWarning> Warnings { get; } = [];
    }
}