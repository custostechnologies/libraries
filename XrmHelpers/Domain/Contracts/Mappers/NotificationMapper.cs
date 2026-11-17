using System.Reflection;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Domain.Resolvers;
using Custos.XrmHelpers.Constants;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Domain.Schemas;
using System.Text.Json;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;
using System.Text;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Infrastructure.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class NotificationMapper<TDto> : INotificationMapper<TDto> where TDto : INotifiableDto, new()
    {
        private readonly ITemplateResolver _templateResolver;
        private readonly INotificationContextFactory _contextFactory;
        private readonly INotificationRequestFactory _requestFactory;
        private readonly INotificationRequestRepository _requestRepo;
        private readonly IDataverseLogger _logger;
        private readonly ITagResolver _tagResolver;
        private readonly IEmailRenderer _renderer;
        public NotificationMapper(
            IDataverseLoggerFactory loggerFactory,
            INotificationContextFactory contextFactory,
            INotificationRequestFactory requestFactory,
            INotificationRequestRepository requestRepo,
            ITemplateResolver templateResolver,
            ITagResolver tagResolver,
            IEmailRenderer renderer
        )
        {
            _logger = loggerFactory.Create<NotificationMapper<TDto>>();
            _templateResolver = templateResolver;
            _contextFactory = contextFactory;
            _requestFactory = requestFactory;
            _requestRepo = requestRepo;
            _tagResolver = tagResolver;
            _renderer = renderer;
        }
        public (string html, NotificationRequestDto request) Render(
            NotificationContext context,
            OperationType type, 
            string recipientEmail, 
            string recipientName,
            IReadOnlyDictionary<string, string?>? customTags = null
        )
        {
            if (context == null) throw new InvalidOperationException("NotificationContext cannot be NULL");
            if (context.Dto == null) throw new InvalidOperationException("NotificationContext.Dto cannot be NULL");

            var template = _templateResolver.Resolve(context.Dto.GetType(), type);
            var tags = _tagResolver.BuildTags(context, template);
            if (customTags != null)
            {
                foreach (var customTag in customTags)
                {
                    var existingTag = tags.FirstOrDefault(tag => tag.InjectionToken == customTag.Key);
                    if (existingTag != null)
                    {
                        existingTag.Value = customTag.Value;
                    }
                    else
                    {
                        tags.Add(new ContextInjectionTag
                        {
                            InjectionToken = customTag.Key,
                            ContextKey = customTag.Key,
                            Value = customTag.Value
                        });
                    }
                }
            }

            var html = _renderer.Render(template, tags);

            if (template.EmailTemplate == null) throw new Exception("Email Template was resolved to NULL.");
            var entityId = type == OperationType.SummaryNotification
                ? Guid.Empty
                : context.Dto.EntityId;

            var request = _requestFactory.Create(
                template.EmailTemplate,
                entityId,
                recipientEmail,
                context.CC
            );

            return (html, request);
        }
        public Guid CreateNotification(
            TDto dto,
            OperationType type,
            string recipientEmail,
            string recipientName,
            string cc,
            IReadOnlyDictionary<string, string?>? customTags = null
        )
        {
            var context = _contextFactory.Create(
                dto,
                type,
                recipientEmail,
                recipientName,
                cc
            );

            var notification = Render(
                context, 
                type, 
                recipientEmail, 
                recipientName,
                customTags
            );

            return _requestRepo.SaveRequest(notification.request, notification.html);
        }
        public Guid CreateSummaryNotification(
            IReadOnlyCollection<TDto> dtos,
            OperationType type,
            string recipientEmail,
            string recipientName,
            string cc,
            string? metricsTableHtml,
            IReadOnlyDictionary<string, string?>? customTags = null
        )
        {
            var context = _contextFactory.CreateSummary(
                dtos,
                type,
                recipientEmail,
                recipientName,
                cc
            );

            var joinedTable = string.IsNullOrWhiteSpace(metricsTableHtml)
                ? ""
                : @$"
                    <h3>Metrics</h3>
                    {metricsTableHtml}
                    </br></br>
                    {context.Table}
                ";

            var joinedContext = context;
            joinedContext.Table = joinedTable;

            var finalContext = string.IsNullOrWhiteSpace(joinedTable)
                ? context
                : joinedContext;

            var notification = Render(
                finalContext, 
                type, 
                recipientEmail, 
                recipientName,
                customTags
            );

            return _requestRepo.SaveRequest(notification.request, notification.html);
        }
    }
}