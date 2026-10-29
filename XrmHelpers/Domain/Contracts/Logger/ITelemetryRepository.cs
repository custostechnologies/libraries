using Microsoft.Xrm.Sdk.PluginTelemetry;

public interface ITelemetryRepository
{
    void SaveLog(string message);
    Guid CreateLog();
}