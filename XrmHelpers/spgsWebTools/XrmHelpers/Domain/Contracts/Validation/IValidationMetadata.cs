namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IValidationMetadata
    {
        string ValidationRuleSet { get; }
    }
    public interface IValidationError
    {
        public string TargetAttribute { get; set; }
        public string Message { get; set; }
    }
    public interface IValidationWarning
    {
        public string TargetAttribute { get; set; }
        public string Message { get; set; }
    }
    public interface IValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationError> Errors { get; }
        public List<ValidationWarning> Warnings { get; }
    }
}