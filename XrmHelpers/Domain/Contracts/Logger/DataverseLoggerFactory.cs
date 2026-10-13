using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace spgsWebTools.XrmHelpers
{
    public sealed class DataverseLoggerFactory : IDataverseLoggerFactory
    {
        private readonly ITracingService _tracer;
        private readonly ILogger _telemetry;

        public DataverseLoggerFactory(
            ITracingService tracer,
            ILogger telemetry
        )
        {
            _tracer = tracer;
            _telemetry = telemetry;
        }

        public IDataverseLogger Create<T>()
        {
            return new DataverseLogger(
                _tracer,
                _telemetry,
                typeof(T).Name
            );
        }
    }
}