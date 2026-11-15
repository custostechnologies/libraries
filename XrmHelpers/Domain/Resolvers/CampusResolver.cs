using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Shared.Utilities;

namespace custos.XrmHelpers.Domain.Resolvers
{
    public class CampusResolver
    {
        public static Campus ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                Campus result = safe switch
                {
                    "pk" or "prekindergarten" or "prek" => Campus.PreK,
                    "j" or "junior" or "juniorschool" => Campus.Junior,
                    "s" or "secondary" or "secondaryschool" => Campus.Secondary,
                    "b" or "acrosstheschool" => Campus.AcrossTheSchool,
                    _ => Campus.OtherOrNone
                };

                return result;
            }
            catch (Exception)
            {
                return Campus.ResolutionError;
            }
        }
    }
}