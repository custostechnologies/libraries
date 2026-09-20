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

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationMapper<TDto> : INotificationMapper<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseContext _context;
        private readonly ILookupEngine _lookupEngine;
        private readonly DataverseLogger _logger;
        public NotificationMapper(
            IDataverseContext context,
            ILookupEngine lookupEngine
        )
        {
            _context = context;
            _logger = new DataverseLogger(_context.Tracer, typeof(DataverseMapper<TDto>).Name);
            _lookupEngine = lookupEngine;
        }
        public string CreateHtmlObject(TDto dto)
        {
            string htmlContent = string.Empty;
            string templateKey = string.Empty;

            EntityCollection queryResults;
            Entity emailTemplate;
            byte[] htmlTemplateBytes;

            FileDownloaderService downloaderService = new FileDownloaderService(_context);

            // retrieve email template
            if (dto.GetType() == typeof(SafetyIncidentDto))
            {
                templateKey = "newSafetyIncident";
            }

            QueryExpression queryExpression = new QueryExpression(EntityLogicalNames.EmailTemplate)
            {
                TopCount = 1,
                ColumnSet = EmailTemplateSchema.AllColumns,
                Criteria = new FilterExpression()
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            EmailTemplateFields.EmailTemplateKey,
                            ConditionOperator.Equal,
                            templateKey
                        )
                    }
                }
            };

            if (!string.IsNullOrWhiteSpace(templateKey))
            {
                queryResults = _context.Service.RetrieveMultiple(queryExpression);
                if (queryResults.Entities.Count == 0)
                {
                    _logger.WriteLog("No email templates returned.");
                    throw new Exception("No email templates returned.");
                }
                else
                {
                    emailTemplate = queryResults.Entities[0];

                    _logger.WriteLog($"Email template retrieved. Downloading HTML Template file attachment.");

                    htmlTemplateBytes = downloaderService.Execute(
                        new EntityReference(EntityLogicalNames.EmailTemplate, emailTemplate.Id),
                        EmailTemplateFields.HtmlTemplate
                    );

                    htmlContent = Encoding.UTF8.GetString(htmlTemplateBytes);

                    _logger.WriteLog($"htmlContent: {htmlContent}");
                }
            }
            return htmlContent;
        }
        public Entity MapToNotificationRequest(TDto dto)
        {
            Entity notificationRequest = new Entity(EntityLogicalNames.NotificationRequest);

            _logger.WriteLog($"\nEntered MapToNotificationRequest with typeof(Dto).Name: {typeof(TDto).Name}");

            _logger.WriteLog($"Creating new entity for EntityLogicalName: {dto.TargetEntityLogicalName}");

            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                try
                {
                    object setValue;

                    var link = property.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null) continue;

                    var value = property.GetValue(dto);

                    if (value == null) continue;

                    var targetType = link.TargetType;
                    var srcType = link.SourceType;
                    var dtoValueType = property.PropertyType;

                    if (link.TargetLogicalName == null)
                        continue;

                    var targetAttribute = link.TargetLogicalName;

                    if (property.PropertyType.IsEnum)
                    {
                        setValue = setValue = new OptionSetValue(Convert.ToInt32(value));
                    }
                    else if (dtoValueType == targetType)
                    {
                        setValue = value;
                    }
                    else if (targetType == typeof(OptionSetValue))
                    {
                        setValue = value;
                    }
                    else if (targetType == typeof(string) && dtoValueType == typeof(Guid))
                    {
                        setValue = value.ToString() ?? Guid.Empty.ToString();
                    }
                    else if (dtoValueType == typeof(string) && targetType == typeof(Guid))
                    {
                        setValue = Guid.Parse(value.ToString());
                    }
                    else
                    {
                        _logger.WriteLog(
                            $"Attempting ChangeType." +
                            $" Property={property.Name}" +
                            $" ValueType={value.GetType().FullName}" +
                            $" TargetType={targetType!.FullName}"
                        );
                        setValue = Convert.ChangeType(value, targetType);
                    }

                    notificationRequest[link.TargetLogicalName] = setValue;
                }
                catch (Exception ex)
                {
                    _logger.WriteLog($"Exception: {ex}");
                    if (ex.InnerException != null)
                    {
                        _logger.WriteLog($"InnerException: {ex.InnerException}");
                    }
                    throw;
                }
            }

            _logger.WriteLog($"Constructed new Entity to be sent to Target after determining operation type...");

            return notificationRequest;
        }
    }
}