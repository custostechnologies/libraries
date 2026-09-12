namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class DataverseContext : IDataverseContext
    {
        public DataverseContext(
            IOrganizationService service,
            ITracingService tracer,
            IExecutionContext context
        )
        {
            Service = service;
            Tracer = tracer;
            ExecutionContext = context;

            tracer.Trace("[DataverseContext] Instantiated DataverseContext class.");
        }
        public IOrganizationService Service { get; }
        public ITracingService Tracer { get; }
        public IExecutionContext ExecutionContext { get; }
    }
}