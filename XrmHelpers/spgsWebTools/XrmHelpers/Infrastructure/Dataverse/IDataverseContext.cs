namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public interface IDataverseContext
    {
        public IOrganizationService Service { get; }
        public ITracingService Tracer { get; }
        public IExecutionContext ExecutionContext { get; }
    }
}