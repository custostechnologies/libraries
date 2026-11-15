using custos.XrmHelpers.Domain.Contracts;

namespace custos.FunctionHelpers.Types
{
    public interface IAuthorisationService
    {
        Task<IAuthValidationResult> ValidateIncomingRequestAsync(string token);
    }
}