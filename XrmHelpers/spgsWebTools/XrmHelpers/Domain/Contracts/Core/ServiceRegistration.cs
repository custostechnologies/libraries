using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Services;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddXrmHelpers(
            this IServiceCollection services)
        {
            services.AddSingleton<IDataverseContext, DataverseContext>();

            services.AddSingleton<IValidationEngine, ValidationEngine>();

            services.AddSingleton<ILookupEngine, LookupEngine>();

            services.AddSingleton<ILookupMetadataFactory, LookupMetadataFactory>();

            services.AddTransient(typeof(ILookupResolver<>), typeof(LookupResolver<>));

            services.AddSingleton(typeof(IDataverseMapper<>), typeof(DataverseMapper<>));

            services.AddSingleton(typeof(ILookupEnricher<>), typeof(LookupEnricher<>));

            services.AddSingleton(typeof(IHashService<>), typeof(HashService<>));

            services.AddSingleton(typeof(IChangeDetectionService<>), typeof(ChangeDetectionService<>));

            return services;
        }
    }
}