using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public interface IDataverseContext
    {
        IOrganizationService Service { get; }
        ITracingService Tracer { get; }
        IExecutionContext ExecutionContext { get; }
    }
}