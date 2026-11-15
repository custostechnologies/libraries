using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Shared.Utilities;

namespace custos.XrmHelpers.Domain.Resolvers
{
    public class StatusReasonResolver
    {
        public static StatusReason ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                StatusReason result = safe switch
                {
                    "active" => StatusReason.Active,
                    "inactive" => StatusReason.Inactive,
                    _ => StatusReason.Inactive
                };

                return result;
            }
            catch (Exception)
            {
                return StatusReason.Inactive;
            }
        }
    }
}