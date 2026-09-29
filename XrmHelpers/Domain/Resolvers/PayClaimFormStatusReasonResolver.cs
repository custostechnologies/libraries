using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.Domain.Resolvers
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
    }
}