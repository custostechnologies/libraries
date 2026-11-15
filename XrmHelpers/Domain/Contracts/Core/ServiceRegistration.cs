using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Services;
using Custos.XrmHelpers.Infrastructure.Dataverse;

namespace Custos.XrmHelpers.Domain
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