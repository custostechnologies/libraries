using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Domain.Resolvers
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