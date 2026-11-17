using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;

using Microsoft.Xrm.Sdk;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class NotificationRequestFactory : INotificationRequestFactory
    {
        private readonly EnvironmentVariableValues _environmentVariableValues;
        private const string DEBUGGING_STATUS_SCHEMA_NAME = "mthomas_envVar_debuggingService_debuggingMode";
        private const string DEBUGGING_EMAIL_SCHEMA_NAME = "mthomas_envVar_debuggingService_emailAddress";
        private readonly IDataverseLogger _logger;
        public NotificationRequestFactory(
            EnvironmentVariableValues environmentVariableValues,
            IDataverseLoggerFactory dataverseLoggerFactory
        ){
            _environmentVariableValues = environmentVariableValues;
            _logger = dataverseLoggerFactory.Create<NotificationRequestFactory>();
        }
        public NotificationRequestDto Create(
            EmailTemplateDto template,
            Guid entityId,
            string recipientEmail,
            string cc
        )
        {
            _logger.WriteLog($"Check notification debugging status...");
            var debuggingMode = _environmentVariableValues.GetValue<bool>(DEBUGGING_STATUS_SCHEMA_NAME).Value;

            var debuggingAddress = debuggingMode 
                ? _environmentVariableValues.GetValue<string>(DEBUGGING_EMAIL_SCHEMA_NAME).Value?.Trim() ?? ""
                : "";

            _logger.WriteLog($"Notification debugging mode: {debuggingMode}");

            if (debuggingMode && string.IsNullOrWhiteSpace(debuggingAddress))
            {
                throw new InvalidPluginExecutionException(
                    $"Debugging mode is enabled but environment variable '{DEBUGGING_EMAIL_SCHEMA_NAME}' is empty."
                );
            }

            var originalTo = recipientEmail?.Trim() ?? "";

            var originalCC = string.IsNullOrWhiteSpace(cc) 
                ? template.CC?.Trim()
                : cc?.Trim();

            var resolvedCC = debuggingMode
                ? ""
                : originalCC; 

            var resolvedTo = debuggingMode 
                ? debuggingAddress
                : originalTo;

            var subject = debuggingMode
                ? $"[TO: {originalTo}] [CC: {originalCC}] {template.Subject}"
                : template.Subject;

            if (debuggingMode)
            {
                _logger.WriteLog($"Redirecting notification from original to: {originalTo} and cc: {originalCC} to debugging address: {debuggingAddress}");
            }

            return new NotificationRequestDto()
            {
                RequestTitle = template.Name,
                EmailTemplate = new EntityReference(
                    EntityLogicalNames.EmailTemplate,
                    template.EmailTemplateId ?? Guid.Empty
                ),
                RegardingRecordId = entityId,
                CCString = resolvedCC,
                Importance = 0,
                Subject = subject,
                SendFrom = template.SendFrom,
                NotificationType = Enums.NotificationType.Email,
                RecipientString = resolvedTo
            };
        }
    }
}