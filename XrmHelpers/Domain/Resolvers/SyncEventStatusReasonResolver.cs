using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Shared.Utilities;

namespace custos.XrmHelpers.Domain.Resolvers
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