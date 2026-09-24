using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class DataverseContext : IDataverseContext
    {
        public DataverseContext(
            IOrganizationService service,
            ILogger logger,
            IPluginExecutionContext executionContext
        )
        {
            Service = service;
            Logger = logger;
            ExecutionContext = executionContext;

            logger.LogInformation("[DataverseContext] Instantiated DataverseContext class.");
        }
        public IOrganizationService Service { get; }
        public ILogger Logger { get; }
        public IPluginExecutionContext ExecutionContext { get; }
        public DataverseLogger GetLogger<T>()
        {
            return new DataverseLogger(
                Logger,
                typeof(T).Name
            );
        }
    }
}