using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Domain.Resolvers
{
    public class AssignmentSyncEventStatusResolver
    {
        public static Status ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                Status result = safe switch
                {
                    "active" => Status.Active,
                    "inactive" => Status.Inactive,
                    _ => Status.Inactive
                };

                return result;
            }
            catch (Exception)
            {
                return Status.Active;
            }
        }
    }
}