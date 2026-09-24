using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Initialisers
{
    public class XrmOrganisationService
    {
        private readonly IPluginExecutionContext _pluginExecutionContext;
        private readonly IOrganizationServiceFactory _organisationServiceFactory;
        private readonly DataverseLogger _logger;
        public XrmOrganisationService(
            IPluginExecutionContext context,
            IOrganizationServiceFactory factory,
            DataverseLogger logger
        )
        {
            _pluginExecutionContext = context;
            _organisationServiceFactory = factory;
            _logger = logger;
        }
        public IOrganizationService Init()
        {
            var orgService = _organisationServiceFactory.CreateOrganizationService(_pluginExecutionContext.UserId);

            _logger.WriteLog("Organisation service initialised, returning to caller...");

            return orgService;

        }
    }
}