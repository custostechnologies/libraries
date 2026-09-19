using System.ComponentModel.DataAnnotations;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IValidationRule<in T>
    {
        ValidationResult Validate(T entity);
        //string TargetAttribute { get; }
        //string ErrorMessage { get; }
        //bool IsValid(T entity);
    }
}