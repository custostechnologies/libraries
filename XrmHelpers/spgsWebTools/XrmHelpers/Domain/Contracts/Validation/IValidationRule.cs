namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IValidationRule<in T>
    {
        public string ErrorMessage { get; }
        public bool IsValid(T entity);
    }
}