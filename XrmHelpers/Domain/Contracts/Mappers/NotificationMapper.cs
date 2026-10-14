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
        private readonly IDataverseLogger _logger;
        private readonly ITagResolver _tagResolver;
        private readonly IEmailRenderer _renderer;
        public NotificationMapper(
            IDataverseLoggerFactory loggerFactory,
            INotificationContextFactory contextFactory,
            INotificationRequestFactory requestFactory,
            ITemplateResolver templateResolver,
            ITagResolver tagResolver,
            IEmailRenderer renderer
        )
        {
            _logger = loggerFactory.Create<NotificationMapper<TDto>>();
            _templateResolver = templateResolver;
            _contextFactory = contextFactory;
            _requestFactory = requestFactory;
            _tagResolver = tagResolver;
            _renderer = renderer;
        }
        public (string html, NotificationRequestDto request) CreateHtmlObject(
            TDto dto,
            OperationType type, 
            string recipientEmail, 
            string recipientName
        )
        {
            var context = _contextFactory.Create(
                dto,
                type,
                recipientEmail,
                recipientName
            );

            var template = _templateResolver.Resolve(dto.GetType());
            var tags = _tagResolver.BuildTags(context, template);
            var html = _renderer.Render(template, tags);

            if (template.EmailTemplate == null) throw new Exception("Email Template was resolved to NULL.");

            var request = _requestFactory.Create(
                template.EmailTemplate,
                dto.EntityId,
                recipientEmail
            );

            return (html, request);
        }
        public (string html, NotificationRequestDto request) CreateSummaryHtmlObject(
            List<TDto> dtos,
            OperationType type,
            string recipientEmail,
            string recipientName
        )
        {
            var context = _contextFactory.CreateSummary(
                dtos,
                type,
                recipientEmail,
                recipientName
            );

            if (context.Dto == null) throw new Exception("Reference DTO unable to be retreived.");
            
            var template = _templateResolver.Resolve(context.Dto.GetType());
            var tags = _tagResolver.BuildTags(context, template);
            var html = _renderer.Render(template, tags);

            if (template.EmailTemplate == null) throw new Exception("Email Template was resolved to NULL.");

            var request = _requestFactory.Create(
                template.EmailTemplate,
                context.Dto.EntityId,
                recipientEmail
            );

            return (html, request);
        }
    }
}