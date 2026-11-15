using System.Net;

using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Infrastructure.Dataverse;
using custos.XrmHelpers.Infrastructure.Dataverse.Services;
using custos.XrmHelpers.Infrastructure.Services;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class NotificationContextFactory : INotificationContextFactory
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly IStyleProvider _styleProvider;
        private readonly IActivityLinkService _activityLinkService;
        public NotificationContextFactory(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            IStyleProvider styleProvider,
            IActivityLinkService activityLinkService
        )
        {
            _context = context;
            _styleProvider = styleProvider;
            _activityLinkService = activityLinkService;
            _logger = loggerFactory.Create<NotificationContextFactory>();
        }
        public NotificationContext Create<TDto>(
            TDto dto,
            OperationType type,
            string recipientAddress,
            string recipientName,
            string cc
        ) where TDto : INotifiableDto
        {
            _logger.WriteLog($"Generating record link from dto class with: {dto.SourceEntityLogicalName}");

            var link = dto.LinkToRecord(
                _context,
                dto.SourceEntityLogicalName,
                dto.EntityId,
                dto.TargetAppDisplayName
            );

            if (dto is IActivityDto activity)
            {
                var html = _activityLinkService.CreateLinkHtml(
                    activity.RegardingRecordEntityLogicalName,
                    activity.RegardingName ?? activity.Regarding?.Name ?? "Link to Record",
                    activity.RegardingId,
                    activity.RegardingRecordAppDisplayName
                );
                activity.RegardingRecordDisplayLink = html;
            }

            var table = SummaryNotificationService.BuildSingleEntityTable(
                dto,
                type,
                _styleProvider
            );

            return new NotificationContext()
            {
                Dto = dto,
                To = recipientAddress,
                CC = cc,
                RecipientName = recipientName,
                Table = table,
                ButtonText = $"Open in {dto.TargetAppDisplayName}",
                ButtonUrl = link
            };
        }

        public NotificationContext CreateSummary<TDto>(
            IReadOnlyCollection<TDto> dtos,
            OperationType type,
            string recipientAddress,
            string recipientName,
            string cc
        ) where TDto : INotifiableDto
        {
            _logger.WriteLog($"Generating record links from dtos.");
            Dictionary<Guid, string> links = new Dictionary<Guid, string>();

            if (dtos == null || dtos.Count == 0) throw new Exception();
            
            foreach(var dto in dtos)
            {
                _logger.WriteLog(
                    $"EntityId: {dto.EntityId}, EntityName: {dto.SourceEntityLogicalName}, TargetAppDisplayName: {dto.TargetAppDisplayName}"
                );

                var link = dto.LinkToRecord(
                    _context,
                    dto.SourceEntityLogicalName,
                    dto.EntityId,
                    dto.TargetAppDisplayName
                );

                _logger.WriteLog("Adding link to collection.");
                
                links.Add(dto.EntityId, link);
            } 

            _logger.WriteLog("Building HTML table");

            var table = SummaryNotificationService.BuildMultiEntityTable(
                dtos,
                links,
                type,
                _styleProvider
            );

            // CREATE METRICS TABLE

            _logger.WriteLog(
                $"Returning new NotificationContext object with To: {recipientAddress}, CC: {cc}, RecipientName: {recipientName}"
            );

            return new NotificationContext()
            {
                Dto = dtos.First(),
                To = recipientAddress,
                CC = cc,
                RecipientName = recipientName,
                Table = table,
                ButtonText = "Help Me",
                ButtonUrl = ""
            };
        }
    }
}