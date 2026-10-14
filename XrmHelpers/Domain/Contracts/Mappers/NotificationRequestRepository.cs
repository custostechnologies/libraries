using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;
using Microsoft.Xrm.Sdk;
using System.Reflection;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationRequestRepository : INotificationRequestRepository
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly FileUploaderService _uploaderService;
        public NotificationRequestRepository(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            FileUploaderService uploaderService
        )
        {
            _context = context;
            _logger = loggerFactory.Create<NotificationRequestRepository>();
            _uploaderService = uploaderService;
        }
        public Guid SaveRequest(
            NotificationRequestDto dto,
            string content
        )
        {
            Entity notificationRequest = new Entity(EntityLogicalNames.NotificationRequest);

            _logger.WriteLog($"\nEntered MapToNotificationRequest with typeof(Dto).Name: {typeof(NotificationRequestDto).Name}");

            var properties = dto.GetType().GetProperties();
            _logger.WriteLog($"Retrieved DTO properties, length: {properties.Length}");

            foreach (var property in properties)
            {
                try
                {
                    object setValue;

                    var link = property.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null) continue;

                    if (string.IsNullOrWhiteSpace(link.TargetLogicalName))
                    {
                        continue;
                    }

                    _logger.WriteLog($"Mapping property: {link.TargetLogicalName}");

                    var value = property.GetValue(dto);

                    _logger.WriteLog(
                        $"Property={property.Name}, " +
                        $"Type={property.PropertyType.FullName}, " +
                        $"Value={value}"
                    );

                    if (value == null) continue;

                    if (link.TargetType == null)
                    {
                        _logger.WriteLog($"TargetType null for property {property.Name}");
                        continue;
                    }

                    var targetType = Nullable.GetUnderlyingType(link.TargetType) ?? link.TargetType;
                    var dtoValueType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    if (link.TargetLogicalName == null)
                        continue;

                    if (property.PropertyType.IsEnum)
                    {
                        _logger.WriteLog(
                            $"ENUM: {property.Name}, " +
                            $"Value={value}, " +
                            $"IntValue={Convert.ToInt32(value)}"
                        );
                        setValue = new OptionSetValue(Convert.ToInt32(value));
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
                        if (!Guid.TryParse(
                            value?.ToString(),
                            out Guid guidValue))
                        {
                            continue;
                        }

                        setValue = guidValue;
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
                    throw;
                }
            }

            _logger.WriteLog($"Constructed new Entity to be sent to Target after determining operation type...");

            var newId = _context.Service.Create(notificationRequest);

            if (newId == Guid.Empty)
            {
                _logger.WriteLog($"Unable to create new notification request entity");
            }
            else
            {
                _uploaderService.Execute(
                    content,
                    new EntityReference(
                        EntityLogicalNames.NotificationRequest,
                        newId
                    ),
                    NotificationRequestFields.HTMLDocument,
                    "content.html"
                );
            }

            return newId;
        }
    }
}