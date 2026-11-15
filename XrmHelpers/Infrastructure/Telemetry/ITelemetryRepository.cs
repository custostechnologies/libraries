using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace custos.XrmHelpers.Infrastructure.Telemetry
{
    public interface ITelemetryRepository
    {
        void SaveLog(string message);
        Guid CreateLog();
    }
}