using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public interface IDataverseContext
    {
        IPluginRuntime Runtime { get; }
        IDataverseLogger GetLogger<T>();
    }
}