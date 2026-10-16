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

namespace spgsWebTools.XrmHelpers.Infrastructure.Services
{
    public sealed class RegistrationServices
    {
        public IServiceProvider BuildXrmServices(ICustomLocalPluginContext localContext)
        {

            try
            {
                var services = new ServiceCollection();
                localContext.Trace("Built services");

                services.AddSingleton<IPluginExecutionContext>(localContext.PluginExecutionContext);
                services.AddSingleton<IExecutionContext>(localContext.PluginExecutionContext);
                services.AddSingleton<IOrganizationService>(localContext.PluginUserService);
                services.AddSingleton<ITracingService>(localContext.TracingService);
                services.AddSingleton<ILogger>(localContext.Logger);
                services.AddSingleton<IDataverseContext, DataverseContext>();
                services.AddSingleton<IDataverseLoggerFactory, DataverseLoggerFactory>();
                
                // add query registrations
                services.AddSingleton<EmailTemplates>();
                services.AddSingleton<Themes>();
                services.AddSingleton<TemplateSnippetIntersects>();
                services.AddSingleton<CoreStaffMembers>();
                services.AddSingleton<EnvironmentVariableValues>();

                // add notification service registrations
                services.AddSingleton<INotificationContextFactory, NotificationContextFactory>();
                services.AddSingleton<INotificationRequestFactory, NotificationRequestFactory>();
                services.AddSingleton<INotificationRequestRepository, NotificationRequestRepository>();
                services.AddSingleton<IEmailRenderer, EmailRenderer>();
                services.AddSingleton<ITemplateResolver, TemplateResolver>();
                services.AddSingleton<ITagResolver, TagResolver>();

                // add style provider registration
                services.AddSingleton<IStyleProvider, StyleProvider>();

                // add file service registrations
                services.AddSingleton<FileUploaderService>();
                services.AddSingleton<FileDownloaderService>();

                services.AddXrmHelpers();

                return services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in BuildXrmServices: {ex}");
                throw;
            }
        }
    }
}