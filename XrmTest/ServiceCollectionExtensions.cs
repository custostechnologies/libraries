using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Serialisation;

namespace spgsWebTools.XrmHelpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddXrmSerializationEngine(this IServiceCollection services)
        {
            // structural component registrations
            services.AddScoped<IValidationEngine, ValidationEngine>();

            // synchronisation engine registration
            //services.AddScoped(typeof(SyncEngine<>));

            // rule set provider registrations
            services.AddScoped<object, StrictStudentRuleSet>();

            return services;
        }
    }
}
