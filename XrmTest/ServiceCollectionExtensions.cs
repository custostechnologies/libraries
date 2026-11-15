using Microsoft.Extensions.DependencyInjection;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Serialisation;

namespace Custos.XrmHelpers
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
