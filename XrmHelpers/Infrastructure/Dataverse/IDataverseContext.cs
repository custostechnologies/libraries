using Custos.XrmHelpers.Infrastructure.Dataverse.Services;

namespace Custos.XrmHelpers.Infrastructure.Dataverse
{
    public interface IDataverseContext
    {
        IPluginRuntime Runtime { get; }
        IDataverseLogger GetLogger<T>();
    }
}