using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.FunctionHelpers.Types
{
    public interface IAuthorisationService
    {
        Task<IAuthValidationResult> ValidateIncomingRequestAsync(string token);
    }
}