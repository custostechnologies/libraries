using Microsoft.Extensions.DependencyInjection;
using custos.XrmHelpers.Domain.Contracts;
using custos.XrmHelpers.Domain.Dtos;
using custos.XrmHelpers.Serialisation;

namespace custos.XrmHelpers
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
