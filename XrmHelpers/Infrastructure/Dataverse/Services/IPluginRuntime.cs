using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Services
{
    public interface IPluginRuntime
    {
        IPluginExecutionContext ExecutionContext { get; }
        IOrganizationService UserService { get; }
        ITracingService TracingService { get; }
        ILogger Logger { get; }
    }
}