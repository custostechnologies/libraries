using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using spgsWebTools.XrmHelpers.Domain;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Services;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Telemetry;
using spgsWebTools.XrmHelpers.Infrastructure.Graph;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Factories;

namespace spgsWebTools.XrmHelpers.Infrastructure.Services
{
    public static class ServiceRegistry
    {
        public static IServiceCollection BuildCoreXrmServices(
            this IServiceCollection services
        )
        {
            try
            {
                Console.WriteLine("ENTERED BuildCoreXrmServices()");
                services.AddScoped<IDataverseLoggerFactory, DataverseLoggerFactory>();

                Console.WriteLine("REGISTERING ITelemetryRepository");
                services.AddScoped<ITelemetryRepository, TelemetryRepository>();
                services.AddScoped<IDataverseContext, DataverseContext>();

                services.AddScoped<IValidationEngine, ValidationEngine>();
                services.AddScoped<ILookupEngine, LookupEngine>();
                services.AddScoped<ILookupMetadataFactory, LookupMetadataFactory>();

                services.AddScoped(typeof(ILookupResolver<>), typeof(LookupResolver<>));
                services.AddScoped(typeof(ILookupEnricher<>), typeof(LookupEnricher<>));
                services.AddScoped(typeof(IDataverseMapper<>), typeof(DataverseMapper<>));
                services.AddScoped(typeof(IQueryMapper<>), typeof(QueryMapper<>));

                services.AddScoped<CoreStaffMembers>();
                services.AddScoped<EnvironmentVariableValues>();

                // add query registrations
                services.AddScoped<EmailTemplates>();
                services.AddScoped<Themes>();
                services.AddScoped<TemplateSnippetIntersects>();
                services.AddScoped<Systemusers>();
                services.AddScoped<Teams>();
                services.AddScoped<ProcessStageMetadata>();

                // add notification service registrations
                services.AddScoped(typeof(INotificationMapper<>), typeof(NotificationMapper<>));
                services.AddScoped(typeof(INotificationPipeline<>), typeof(NotificationPipeline<>));
                services.AddScoped(typeof(DigestQueryEngine<>));
                services.AddScoped<INotificationContextFactory, NotificationContextFactory>();
                services.AddScoped<INotificationRequestFactory, NotificationRequestFactory>();
                services.AddScoped<INotificationRequestRepository, NotificationRequestRepository>();
                services.AddScoped<IEmailRenderer, EmailRenderer>();
                services.AddScoped<ITemplateResolver, TemplateResolver>();
                services.AddScoped<ITagResolver, TagResolver>();

                // digest configurations
                services.AddScoped<IDigestConfiguration<SafetyIncidentDto>, SafetyIncidentDigestConfiguration>();
                services.AddScoped<IDigestConfiguration<ChildProtectionConcernDto>, ConcernDigestConfiguration>();
                services.AddScoped<IDigestConfiguration<TaskDto>, TaskDigestConfiguration>();
                services.AddScoped<IDigestConfiguration<WorkersCompensationClaimDto>, WorkersCompensationClaimDigestConfiguration>();

                services.AddScoped(typeof(IHashService<>), typeof(HashService<>));
                services.AddScoped(typeof(IChangeDetectionService<>), typeof(ChangeDetectionService<>));
                services.AddScoped(typeof(ISyncPipeline<>), typeof(SyncPipeline<>));

                // workers comp claim factory
                services.AddScoped<WorkersCompFactory>();

                services.AddScoped<IActivityLinkService, ActivityLinkService>();
                

                // add mappers
                services.AddTransient<SynJobPositionMapper>();

                // add style provider registration
                services.AddScoped<IStyleProvider, StyleProvider>();

                // add file service registrations
                services.AddScoped<FileUploaderService>();
                services.AddScoped<FileDownloaderService>();

                // add email service
                services.AddScoped<EmailService>();

                return services;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in BuildXrmServices: {ex}");
                throw;
            }
        }
    }
}