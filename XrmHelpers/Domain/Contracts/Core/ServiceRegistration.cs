using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.DependencyInjection;
using custos.XrmHelpers;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain;
using custos.XrmHelpers.Domain.Dtos;
using custos.XrmHelpers.Domain.Contracts;
using custos.XrmHelpers.Domain.Services;
using custos.XrmHelpers.Infrastructure.Dataverse;

namespace custos.XrmHelpers.Domain
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddXrmHelpers(
            this IServiceCollection services)
        {
            services.AddScoped<IDataverseContext, DataverseContext>();

            services.AddSingleton<IValidationEngine, ValidationEngine>();

            services.AddSingleton<ILookupEngine, LookupEngine>();

            services.AddSingleton<ILookupMetadataFactory, LookupMetadataFactory>();

            services.AddTransient(typeof(ILookupResolver<>), typeof(LookupResolver<>));

            services.AddSingleton(typeof(IDataverseMapper<>), typeof(DataverseMapper<>));

            services.AddSingleton(typeof(INotificationMapper<>), typeof(NotificationMapper<>));

            services.AddSingleton(typeof(ILookupEnricher<>), typeof(LookupEnricher<>));

            services.AddSingleton(typeof(IHashService<>), typeof(HashService<>));

            services.AddSingleton(typeof(IChangeDetectionService<>), typeof(ChangeDetectionService<>));

            services.AddSingleton(typeof(ISyncPipeline<>), typeof(SyncPipeline<>));

            services.AddSingleton(typeof(INotificationPipeline<>), typeof(NotificationPipeline<>));

            return services;
        }
    }
}