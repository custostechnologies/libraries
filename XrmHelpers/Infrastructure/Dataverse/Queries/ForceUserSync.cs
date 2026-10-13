using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries
{
    public class ForceUserSync
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public ForceUserSync(IDataverseContext context, IDataverseLoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.Create<ForceUserSync>();
        }
        public Guid? Execute(string upn)
        {
            try
            {
                var request = new OrganizationRequest("RetrieveUserFromAzureActiveDirectory")
                {
                    ["Email"] = upn
                };

                OrganizationResponse response = _context.Service.Execute(request);

                if (response.Results.Contains("SystemUserId"))
                {
                    Guid systemuserid = (Guid)response.Results["SystemUserId"];

                    return systemuserid;
                }
            }
            catch (FaultException<OrganizationServiceFault> exception)
            {
                throw new InvalidPluginExecutionException("An error occured in the ChangeDetection plugin.", exception);
            }
            return null;
        }
    }
}