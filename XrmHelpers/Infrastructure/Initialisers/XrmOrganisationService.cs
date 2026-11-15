using System;
using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Infrastructure.Initialisers
{
    public class XrmOrganisationService
    {
        private readonly IPluginExecutionContext _pluginExecutionContext;
        private readonly IOrganizationServiceFactory _organisationServiceFactory;
        private readonly IDataverseLogger _logger;
        public XrmOrganisationService(
            IPluginExecutionContext context,
            IOrganizationServiceFactory factory,
            IDataverseLogger logger
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