using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace Custos.XrmHelpers.Infrastructure.Telemetry
{
    public interface ITelemetryRepository
    {
        void SaveLog(string message);
        Guid CreateLog();
    }
}