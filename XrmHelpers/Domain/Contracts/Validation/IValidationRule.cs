using System.ComponentModel.DataAnnotations;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IValidationRule<in T>
    {
        ValidationResult Validate(T entity);
        //string TargetAttribute { get; }
        //string ErrorMessage { get; }
        //bool IsValid(T entity);
    }
}