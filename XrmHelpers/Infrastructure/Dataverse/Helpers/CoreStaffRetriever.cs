
using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Shared.Validation;

namespace Custos.XrmHelpers.Infrastructure.Dataverse
{
    public class CoreStaffRetriever
    {
        private readonly CoreStaffMembers _coreStaffMemberQuery;
        private readonly IDataverseLogger _logger;
        public CoreStaffRetriever(
            IDataverseLoggerFactory loggerFactory,
            CoreStaffMembers coreStaffMemberQuery
        )
        {
            _coreStaffMemberQuery = coreStaffMemberQuery;
            _logger = loggerFactory.Create<CoreStaffRetriever>();
        }
        public (
            bool resolved,
            Entity? coreStaffMember
        ) Retrieve(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.WriteLog("RetrieveUserPreferredName called with a null address. Falling back to default.");
                return (false, null);
            }

            var valid = OrgAddressValidator.Validate(email);

            if (valid)
            {
                Entity staffMember = _coreStaffMemberQuery.GetEntityByOrgEmail(email);

                if (staffMember.Id == Guid.Empty)
                {
                    _logger.WriteLog("Unable to find CoreStaffMember. Returning ");
                    return (false, null);
                }

                _logger.WriteLog($"Resolved CoreStaffMember for address: {email}. Returning true and Entity");
                return (true, staffMember);
            }
            else
            {
                _logger.WriteLog("Invalid email (non-org). Falling back to default.");
                return (false, null);
            }
        }
    }
}