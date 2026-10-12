using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationPipeline<TDto> : INotificationPipeline<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly IDataverseMapper<TDto> _dataverseMapper;
        private readonly INotificationMapper<TDto> _notificationMapper;
        private readonly ILookupEnricher<TDto> _enricher;
        private readonly ISummaryNotificationMapper _summaryMapper;
        private readonly IChangeDetectionService<TDto> _changeDetector;
        public NotificationPipeline(
            IDataverseContext context,
            IDataverseMapper<TDto> dataverseMapper,
            INotificationMapper<TDto> notificationMapper,
            ILookupEnricher<TDto> enricher,
            IChangeDetectionService<TDto> changeDetector,
            ISummaryNotificationMapper summaryMapper
        )
        {
            _context = context;
            _dataverseMapper = dataverseMapper;
            _notificationMapper = notificationMapper;
            _enricher = enricher;
            _changeDetector = changeDetector;
            _summaryMapper = summaryMapper;

            _logger = _context.GetLogger<NotificationPipeline<TDto>>();
        }
        public Guid BuildEmailNotification(Entity target, string to, string? cc)
        {
            TDto dto;

            try
            {
                _logger.WriteLog("Mapping target...");
                dto = _dataverseMapper.MapToDto(target);

                _logger.WriteLog("Enriching target...");
                dto = _enricher.Execute(dto);

                _logger.WriteLog("Creating HTML object...");
                var output = _notificationMapper.CreateHtmlObject(dto);

                NotificationRequestDto requestDto = output.Item2;

                requestDto.RecipientString = to;
                requestDto.CCString = cc;

                _logger.WriteLog("Mapping to NotificationRequest entity...");
                return _notificationMapper.MapToNotificationRequest(output.Item2, output.Item1);
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"Exception: ", ex);
                throw;
            }
        }
    }
}