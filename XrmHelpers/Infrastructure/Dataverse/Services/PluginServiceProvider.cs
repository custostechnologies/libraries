using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;

using Custos.XrmHelpers.Infrastructure.Services;

namespace Custos.XrmHelpers.Infrastructure.Dataverse.Services
{
    public static class PluginServiceProvider
    {
        public static PluginServiceScope CreateProvider(
            IPluginRuntime runtimeContext
        )
        {
            ServiceProvider provider;
            IServiceScope scope;

            var services = new ServiceCollection();

            services.AddScoped<IPluginRuntime>(_ => runtimeContext);
            services.AddScoped<ITracingService>(_ => runtimeContext.TracingService);
            services.AddScoped<IPluginExecutionContext>(_ => runtimeContext.ExecutionContext);
            services.AddScoped<IOrganizationService>(_ => runtimeContext.UserService);
            services.AddScoped<ILogger>(_ => runtimeContext.Logger);

            services.BuildCoreXrmServices();

            try
            {
                provider = services.BuildServiceProvider(
                new ServiceProviderOptions
                {
                    ValidateScopes = true,
                    ValidateOnBuild = true
                }
            );

                scope = provider.CreateScope();
            } catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(
                    $"Dependency Injection Failure: {ex}\n\nInner: {ex.InnerException}"
                );
            }

            return new PluginServiceScope(provider, scope);
        }
    }
}