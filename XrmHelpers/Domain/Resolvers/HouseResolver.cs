using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Shared.Utilities;

namespace custos.XrmHelpers.Domain.Resolvers
{
    public class HouseResolver
    {
        public static House ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                House result = safe switch
                {
                    "ca" or "castlereagh" => House.Castlereagh,
                    "cl" or "claremont" => House.Claremont,
                    "co" or "cornwallis" => House.Cornwallis,
                    "mel" or "melville" => House.Melville,
                    "str" or "strathdon" => House.Strathdon,
                    "wil" or "wilberforce" => House.Wilberforce,
                    _ => throw new Exception($"HouseResolver called with unknown house code: {safe}")
                };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}