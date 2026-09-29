using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Domain.Resolvers
{
    public class StatusResolver
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
                return Status.Inactive;
            }
        }
        public static string ToLabel(int val)
        {
            try
            {
                string result = (uint)val switch
                {
                    0 => "Active",
                    1 => "Inactive",
                    _ => "Unknown"
                };

                return result;
            }
            catch (Exception)
            {
                throw new Exception("Resolver error StatusResolver");
            }
        }
    }
}