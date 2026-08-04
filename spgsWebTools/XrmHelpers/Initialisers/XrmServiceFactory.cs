using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.Shared.XrmHelpers.Initialisers
{
    public class XrmServiceFactory
    {
        private readonly ITracingService _tracer;
        private readonly IServiceProvider _serviceProvider;
        public XrmServiceFactory(
            ITracingService tracer,
            IServiceProvider provider
        )
        {
            _tracer = tracer;
            _serviceProvider = provider;
        }
        public IOrganizationServiceFactory Init()
        {
            var orgServiceFactory = (IOrganizationServiceFactory)_serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            _tracer.Trace("Organisation service factory initialised");

            return orgServiceFactory;
        }
    }
}