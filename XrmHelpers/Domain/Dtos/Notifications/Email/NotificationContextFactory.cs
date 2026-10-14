using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;
using spgsWebTools.XrmHelpers.Infrastructure.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationContextFactory
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly IStyleProvider _styleProvider;
        public NotificationContextFactory(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            IStyleProvider styleProvider
        )
        {
            _context = context;
            _styleProvider = styleProvider;
            _logger = loggerFactory.Create<NotificationContextFactory>();
        }
        public NotificationContext Create<TDto>(
            TDto dto,
            OperationType type,
            string recipientAddress,
            string recipientName
        ) where TDto : INotifiableDto
        {
            _logger.WriteLog($"Generating record link from dto class with: dto.SourceEntityLogicalName");

            var link = dto.LinkToRecord(
                _context,
                dto.SourceEntityLogicalName,
                dto.EntityId,
                dto.TargetAppDisplayName
            );

            var table = SummaryNotificationService.BuildSingleEntityTable(
                dto,
                type,
                _styleProvider
            );

            return new NotificationContext()
            {
                Dto = dto,
                To = recipientAddress,
                RecipientName = recipientName,
                Table = table,
                ButtonText = $"Open in {dto.TargetAppDisplayName}",
                ButtonUrl = link
            };
        }

        public NotificationContext CreateSummary<TDto>(
            List<TDto> dtos,
            OperationType type,
            string recipientAddress,
            string recipientName
        ) where TDto : INotifiableDto
        {
            _logger.WriteLog($"Generating record link from dto class with: dto.SourceEntityLogicalName");
            Dictionary<Guid, string> links = new Dictionary<Guid, string>();

            if (dtos == null || dtos.Count == 0) throw new Exception();
            
            foreach(var dto in dtos)
            {
                var link = dto.LinkToRecord(
                    _context,
                    dto.SourceEntityLogicalName,
                    dto.EntityId,
                    dto.TargetAppDisplayName
                );
                links.Add(dto.EntityId, link);
            } 

            var table = SummaryNotificationService.BuildMultiEntityTable(
                dtos,
                links,
                type,
                _styleProvider
            );

            return new NotificationContext()
            {
                Dto = dtos[0],
                To = recipientAddress,
                RecipientName = recipientName,
                Table = table,
                ButtonText = "Help Me",
                ButtonUrl = ""
            };
        }
    }
}