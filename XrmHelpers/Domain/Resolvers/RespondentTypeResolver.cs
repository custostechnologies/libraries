using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Domain.Resolvers
{
    public class RespondentTypeResolver
    {
        public static RespondentType ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                RespondentType result = safe switch
                {
                    "manager" => RespondentType.Manager,
                    "staffmember" => RespondentType.StaffMember,
                    _ => RespondentType.StaffMember
                };

                return result;
            }
            catch (Exception)
            {
                return RespondentType.StaffMember;
            }
        }
        public static string ToLabel(int val)
        {
            try
            {
                string result = (uint)val switch
                {
                    386630000 => "Staff Member",
                    386630001 => "Manager",
                    _ => "Unknown",
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