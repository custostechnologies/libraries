using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.FunctionHelpers.Types
{
    public interface IAuthorisationService
    {
        Task<IAuthValidationResult> ValidateIncomingRequestAsync(string token);
    }
}