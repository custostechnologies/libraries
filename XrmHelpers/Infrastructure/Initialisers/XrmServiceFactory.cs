using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Initialisers
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
            try
            {
                _logger.WriteLog("Initialising organisation service factory");

                var orgServiceFactory = (IOrganizationServiceFactory)_serviceProvider.GetService(typeof(IOrganizationServiceFactory));

                _logger.WriteLog("Organisation service factory initialised");

                return orgServiceFactory;
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in XrmServiceFactory.Init(): {0}", ex);
                if (ex.InnerException.Message != null)
                {
                    _logger.WriteLog("Inner Exception in XrmServiceFactory.Init(): {0}", ex.InnerException.Message);
                }
                throw;
            }
        }
    }
}