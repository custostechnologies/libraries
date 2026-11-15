using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Domain.Resolvers
{
    public class PayClaimFormStatusReasonResolver
    {
        public static PayClaimFormStatusReason ToEnum(string code)
        {
            try
            {
                string safe = SafeString.Normalise(code);

                PayClaimFormStatusReason result = safe switch
                {
                    "submitted" => PayClaimFormStatusReason.Submitted,
                    "complete" => PayClaimFormStatusReason.Complete,
                    "active" => PayClaimFormStatusReason.Active,
                    "draft" => PayClaimFormStatusReason.Draft,
                    "rejected" => PayClaimFormStatusReason.Rejected,
                    "inactive" => PayClaimFormStatusReason.Inactive,
                    _ => PayClaimFormStatusReason.Active
                };

                return result;
            }
            catch (Exception)
            {
                return PayClaimFormStatusReason.Active;
            }
        }
        public static string ToLabel(int val)
        {
            try
            {
                string result = (uint)val switch
                {
                    386630001 => "Submitted",
                    386630002 => "Complete",
                    1 => "Active",
                    386630004 => "Draft",
                    2 => "Rejected",
                    386630005 => "Inactive",
                    _ => "Unknown"
                };

                return result;
            }
            catch (Exception)
            {
                throw new Exception("Resolver error PayClaimFormStatusReason");
            }
        }
    }
}