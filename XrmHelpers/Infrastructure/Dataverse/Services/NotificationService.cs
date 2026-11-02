using Microsoft.Xrm.Sdk;

using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Models;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services
{
    public class NotificationService<TDto> : INotificationService<TDto>
    {
        private readonly INotificationPipeline<TDto> _pipeline;
        private readonly IDataverseLogger _logger;
        private readonly Systemusers _userQueryService;
        private readonly Teams _teamQueryService;
        public NotificationService(
            IDataverseLoggerFactory loggerFactory,
            INotificationPipeline<TDto> notificationPipeline,
            Systemusers systemusers,
            Teams teams
        )
        {
            _pipeline = notificationPipeline;
            _userQueryService = systemusers;
            _teamQueryService = teams;
            _logger = loggerFactory.Create<NotificationService<TDto>>();
        }
        private User ResolveRecipient(EntityReference ownerRef)
        {
            return _userQueryService.GetMetadataById(ownerRef.Id) 
                ?? throw new InvalidPluginExecutionException($"User returned null from user query service");
        }
        private List<User> ResolveRecipients(EntityReference ownerRef)
        {
            return _teamQueryService.GetMetadataById(ownerRef.Id)?.Members
                ?? throw new InvalidPluginExecutionException($"Team returned null from team query service");
        }
        public void Notify(
            EntityReference ownerRef,
            OperationType operationType,
            Entity notifiableEntity
        )
        {
            _logger.WriteLog("Resolving recipients");

            List<User> recipients;
            switch (ownerRef.LogicalName)
            {
                case "systemuser":
                    recipients = [ResolveRecipient(ownerRef)];
                    break;
                case "team":
                    recipients = ResolveRecipients(ownerRef);
                    break;
                default:
                    throw new InvalidPluginExecutionException($"Unknown EntityReference LogicalName/Owner type: {ownerRef.LogicalName}");
            }

            foreach (var recipient in recipients)
            {
                _logger.WriteLog($"Calling BuildEmailNotification on _notificationPipeline for user (email: {recipient.Email}).");
                _pipeline.BuildEmailNotification(
                    notifiableEntity,
                    operationType,
                    recipient.Email,
                    null,
                    recipient.FirstName
                );
            }
        }
        public void Notify(
            User user,
            OperationType operationType,
            Entity notifiableEntity
        )
        {
            _logger.WriteLog("USER is external or not found in Core Staff Members");

            _logger.WriteLog($"Calling BuildEmailNotification on _notificationPipeline for user (email: {user.Email}).");
            _pipeline.BuildEmailNotification(
                notifiableEntity,
                operationType,
                user.Email,
                null,
                user.FirstName ?? user.FullName
            );
        }
    }
}