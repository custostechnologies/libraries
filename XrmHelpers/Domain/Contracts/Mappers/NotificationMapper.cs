using System.Reflection;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Resolvers;
using spgsWebTools.XrmHelpers.Constants;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Schemas;
using System.Text.Json;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;
using System.Text;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
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
            string recipientName
        )
        {
            if (context == null) throw new InvalidOperationException("NotificationContext cannot be NULL");
            if (context.Dto == null) throw new InvalidOperationException("NotificationContext.Dto cannot be NULL");

            var template = _templateResolver.Resolve(context.Dto.GetType());
            var tags = _tagResolver.BuildTags(context, template);
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
            string cc
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
                recipientName
            );

            return _requestRepo.SaveRequest(notification.request, notification.html);
        }
        public Guid CreateSummaryNotification(
            List<TDto> dtos,
            OperationType type,
            string recipientEmail,
            string recipientName,
            string cc
        )
        {
            var context = _contextFactory.CreateSummary(
                dtos,
                type,
                recipientEmail,
                recipientName,
                cc
            );

            var notification = Render(context, type, recipientEmail, recipientName);
            return _requestRepo.SaveRequest(notification.request, notification.html);
        }
    }
}