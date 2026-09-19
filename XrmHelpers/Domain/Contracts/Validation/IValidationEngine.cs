namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IValidationEngine
    {
        public ValidationResult Validate<T>(T payload) where T : IValidationMetadata;
    }
}