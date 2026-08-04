using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.Shared.XrmHelpers.Initialisers
{
    public class XrmOrganisationService
    {
        private readonly IPluginExecutionContext _pluginExecutionContext;
        private readonly IOrganizationServiceFactory _organisationServiceFactory;
        private readonly ITracingService _tracer;
        public XrmOrganisationService(
            IPluginExecutionContext context,
            IOrganizationServiceFactory factory,
            ITracingService tracer
        )
        {
            _pluginExecutionContext = context;
            _organisationServiceFactory = factory;
            _tracer = tracer;
        }
        public IOrganizationService Init()
        {
            var orgService = _organisationServiceFactory.CreateOrganizationService(_pluginExecutionContext.UserId);

            _tracer.Trace("Organisation service initialised, returning to caller...");

            return orgService;

        }
    }
}