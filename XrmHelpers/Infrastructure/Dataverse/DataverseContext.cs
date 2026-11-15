using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

using custos.XrmHelpers.Infrastructure.Dataverse.Services;

namespace custos.XrmHelpers.Infrastructure.Dataverse
{
    public class DataverseContext : IDataverseContext
    {
        private readonly IPluginRuntime _runtime;
        private readonly IDataverseLoggerFactory _loggerFactory;

        public DataverseContext(
            IPluginRuntime runtime,
            IDataverseLoggerFactory loggerFactory
        )
        {
            _runtime = runtime;
            _loggerFactory = loggerFactory;

            _runtime.TracingService.Trace("[DataverseContext] Instantiated DataverseContext class.");
        }

        public IPluginRuntime Runtime => _runtime;

        public IDataverseLogger GetLogger<T>()
        {
            return _loggerFactory.Create<T>();
        }
    }
}