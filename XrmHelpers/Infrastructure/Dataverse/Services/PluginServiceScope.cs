using Microsoft.Extensions.DependencyInjection;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Services
{
    public sealed class PluginServiceScope : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly IServiceScope _serviceScope;
        public PluginServiceScope(
            ServiceProvider serviceProvider,
            IServiceScope serviceScope
        )
        {
            _serviceProvider = serviceProvider;
            _serviceScope = serviceScope;
        }
        public IServiceProvider Services => _serviceScope.ServiceProvider;
        public void Dispose()
        {
            _serviceScope.Dispose();
            _serviceProvider.Dispose();
        }
    }
}