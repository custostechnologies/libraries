using custos.XrmHelpers.Infrastructure.Dataverse.Services;

namespace custos.XrmHelpers.Infrastructure.Dataverse
{
    public interface IDataverseContext
    {
        IPluginRuntime Runtime { get; }
        IDataverseLogger GetLogger<T>();
    }
}