using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Domain.Resolvers
{
    public class FormTypeResolver
    {
        public static FormType ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                FormType result = safe switch
                {
                    "casual" => FormType.Casual,
                    "salaried" => FormType.Salaried,
                    _ => FormType.NotSelected
                };

                return result;
            }
            catch (Exception)
            {
                return FormType.NotSelected;
            }
        }
        public static string ToLabel(int val)
        {
            try
            {
                string result = (uint)val switch
                {
                    386630000 => "Casual",
                    386630001 => "Salaried",
                    _ => "Not Selected",
                };

                return result;
            }
            catch (Exception)
            {
                throw new Exception("Resolver error RespondentType");
            }
        }
    }
}