using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

using spgsWebTools.XrmHelpers.Infrastructure.Services;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services
{
    public static class PluginServiceProvider
    {
        public static PluginServiceScope CreateProvider(
            IPluginRuntime runtimeContext
        )
        {
            var services = new ServiceCollection();

            services.AddScoped<IPluginRuntime>(_ => runtimeContext);
            services.AddScoped<ITracingService>(_ => runtimeContext.TracingService);
            services.AddScoped<IPluginExecutionContext>(_ => runtimeContext.ExecutionContext);
            services.AddScoped<IOrganizationService>(_ => runtimeContext.UserService);
            services.AddScoped<ILogger>(_ => runtimeContext.Logger);

            services.BuildCoreXrmServices();

            var provider = services.BuildServiceProvider(
                new ServiceProviderOptions
                {
                    ValidateScopes = true,
                    ValidateOnBuild = true
                }
            );

            var scope = provider.CreateScope();

            return new PluginServiceScope(provider, scope);
        }
    }
}