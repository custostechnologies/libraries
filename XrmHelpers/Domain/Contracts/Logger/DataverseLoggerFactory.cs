using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using custos.XrmHelpers.Infrastructure.Telemetry;

namespace custos.XrmHelpers
{
    public sealed class DataverseLoggerFactory : IDataverseLoggerFactory
    {
        private readonly ITracingService _tracer;
        private readonly ILogger _telemetry;
        private readonly ITelemetryRepository _telemetryRepository;

        public DataverseLoggerFactory(
            ITracingService tracer,
            ILogger telemetry,
            ITelemetryRepository telemetryRepository
        )
        {
            _tracer = tracer;
            _telemetry = telemetry;
            _telemetryRepository = telemetryRepository;
        }

        public IDataverseLogger Create<T>()
        {
            return new DataverseLogger(
                _tracer,
                _telemetry,
                _telemetryRepository,
                typeof(T).Name
            );
        }
    }
}