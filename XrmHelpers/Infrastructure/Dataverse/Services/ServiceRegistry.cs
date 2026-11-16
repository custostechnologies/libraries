using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using Custos.XrmHelpers.Domain;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Microsoft.Extensions.DependencyInjection;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Services;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Infrastructure.Telemetry;
using Custos.XrmHelpers.Infrastructure.Graph;
using Custos.XrmHelpers.Infrastructure.Dataverse.Mappers;
using Custos.XrmHelpers.Infrastructure.Dataverse.Factories;

namespace Custos.XrmHelpers.Infrastructure.Services
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
                services.AddScoped(typeof(INotificationService<>), typeof(NotificationService<>));
                services.AddScoped<CoreStaffRetriever>();

                // add query registrations
                services.AddScoped<EmailTemplates>();
                services.AddScoped<Themes>();
                services.AddScoped<TemplateSnippetIntersects>();
                services.AddScoped<HTMLSnippets>();
                services.AddScoped<Systemusers>();
                services.AddScoped<Teams>();
                services.AddScoped<ProcessStageMetadata>();
                services.AddScoped<ChildProtectionConcerns>();
                services.AddScoped<CoreStudents>();
                services.AddScoped<AutoNumberCounters>();
                services.AddScoped<ComplianceItems>();
                services.AddScoped<Policies>();
                services.AddScoped<Risks>();
                services.AddScoped<Hazards>();

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

                // task factory
                services.AddScoped<Dataverse.Factories.TaskFactory>();

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