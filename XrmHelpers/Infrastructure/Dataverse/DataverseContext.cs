using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class DataverseContext : IDataverseContext
    {
        private readonly IDataverseLoggerFactory _loggerFactory;

        public DataverseContext(
            IOrganizationService service,
            ILogger logger,
            IPluginExecutionContext executionContext,
            IDataverseLoggerFactory loggerFactory)
        {
            Service = service;
            Logger = logger;
            ExecutionContext = executionContext;
            _loggerFactory = loggerFactory;

            logger.LogInformation(
                "[DataverseContext] Instantiated DataverseContext class.");
        }

        public IOrganizationService Service { get; }

        public ILogger Logger { get; }

        public IPluginExecutionContext ExecutionContext { get; }

        public IDataverseLogger GetLogger<T>()
        {
            return _loggerFactory.Create<T>();
        }
    }
}