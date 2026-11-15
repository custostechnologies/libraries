using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Shared.Utilities;

namespace custos.XrmHelpers.Domain.Resolvers
{
    public class GenderResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Gender ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                Gender result = safe switch
                {
                    "f" or "female" => Gender.Female,
                    "m" or "male" or "juniorschool" => Gender.Male,
                    "nb" or "nonbinary" or "n" => Gender.NonBinary,
                    "x" or "o" or "other" => Gender.Other,
                    _ => Gender.NotSpecified
                };

                return result;
            }
            catch (Exception)
            {
                return Gender.ResolutionError;
            }
        }
    }
}