using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Initialisers
{
    public class XrmServiceFactory
    {
        private readonly IDataverseLogger _logger;
        private readonly IServiceProvider _provider;
        public XrmServiceFactory(
            IDataverseLogger logger,
            IServiceProvider provider
        )
        {
            _logger = logger;
            _provider = provider;
        }
        public IOrganizationServiceFactory Init()
        {
            if (_provider == null)
            {
                throw new Exception("Null service provider");
            }
            ;
            try
            {
                _logger.WriteLog("Initialising organisation service factory");

                var orgServiceFactory = (IOrganizationServiceFactory)_provider.GetService(typeof(IOrganizationServiceFactory))!;

                _logger.WriteLog("Organisation service factory initialised");

                return orgServiceFactory;
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Exception in XrmServiceFactory.Init(): {0}", ex);
                throw;
            }
        }
    }
}