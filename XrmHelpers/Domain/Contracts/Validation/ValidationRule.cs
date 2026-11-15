namespace custos.XrmHelpers.Domain.Contracts
{
    public abstract class ValidationRule<T> : IValidationRule<T>
    {
        protected ValidationResult Failure(string target, string message)
        {
            var result = new ValidationResult();
            result.Errors.Add(
                new ValidationError(target, message)
            );
            return result;
        }
        protected ValidationResult Success()
        {
            return new ValidationResult();
        }
        public abstract ValidationResult Validate(T entity);
    }
}