using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public interface IDataverseContext
    {
        IOrganizationService Service { get; }
        ILogger Logger { get; }
        IPluginExecutionContext ExecutionContext { get; }
        IDataverseLogger GetLogger<T>();
    }
}