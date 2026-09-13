using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class DataverseContext : IDataverseContext
    {
        public DataverseContext(
            IOrganizationService service,
            ITracingService tracer,
            IPluginExecutionContext executionContext
        )
        {
            Service = service;
            Tracer = tracer;
            ExecutionContext = executionContext;

            tracer.Trace("[DataverseContext] Instantiated DataverseContext class.");
        }
        public IOrganizationService Service { get; }
        public ITracingService Tracer { get; }
        public IPluginExecutionContext ExecutionContext { get; }
    }
}