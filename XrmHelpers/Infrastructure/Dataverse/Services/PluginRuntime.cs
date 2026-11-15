using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Services
{
    public class PluginRuntime : IPluginRuntime
    {
        public PluginRuntime(
            IPluginExecutionContext executionContext,
            IOrganizationService userService,
            ITracingService tracingService,
            ILogger logger
        )
        {
            ExecutionContext = executionContext;
            UserService = userService;
            TracingService = tracingService;
            Logger = logger;
        }
        public IPluginExecutionContext ExecutionContext { get; }
        public IOrganizationService UserService { get; }
        public ITracingService TracingService { get; }
        public ILogger Logger { get; }
    }
}