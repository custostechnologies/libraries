namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IValidationMetadata
    {
        string ValidationRuleSet { get; }
    }
    public interface IValidationError
    {
        public string TargetAttribute { get; }
        public string Message { get; }
    }
    public interface IValidationWarning
    {
        public string TargetAttribute { get; }
        public string Message { get; }
    }
    public interface IValidationResult
    {
        public bool IsValid { get; }
        public List<ValidationError> Errors { get; }
        public List<ValidationWarning> Warnings { get; }
    }
}