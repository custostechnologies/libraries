using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;
using Custos.XrmHelpers.Infrastructure.Services;
using Custos.XrmHelpers.Shared.Validation;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class NotificationPipeline<TDto> : INotificationPipeline<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly IDataverseMapper<TDto> _dataverseMapper;
        private readonly INotificationMapper<TDto> _notificationMapper;
        private readonly ILookupEnricher<TDto> _enricher;
        private readonly IChangeDetectionService<TDto> _changeDetector;
        private readonly CoreStaffMembers _coreStaffMemberQuery;
        private readonly DigestQueryEngine<TDto> _digestQueryEngine;
        private readonly CoreStaffRetriever _coreStaffRetriever;
        private readonly IStyleProvider _styleProvider;
        public NotificationPipeline(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            IDataverseMapper<TDto> dataverseMapper,
            INotificationMapper<TDto> notificationMapper,
            ILookupEnricher<TDto> enricher,
            IChangeDetectionService<TDto> changeDetector,
            CoreStaffMembers coreStaffMemberQuery,
            DigestQueryEngine<TDto> digestQueryEngine,
            CoreStaffRetriever coreStaffRetriever,
            IStyleProvider styleProvider
        )
        {
            _context = context;
            _dataverseMapper = dataverseMapper;
            _notificationMapper = notificationMapper;
            _enricher = enricher;
            _changeDetector = changeDetector;
            _coreStaffMemberQuery = coreStaffMemberQuery;
            _digestQueryEngine = digestQueryEngine;
            _coreStaffRetriever = coreStaffRetriever;
            _styleProvider = styleProvider;
            _logger = loggerFactory.Create<NotificationPipeline<TDto>>();
        }
        public Guid BuildEmailNotification(
            Entity target, 
            OperationType type, 
            string to, 
            string? cc, 
            string fallbackName,
            IReadOnlyDictionary<string, string?>? customTags = null
        )
        {
            TDto dto;

            try
            {
                _logger.WriteLog("Mapping target...");
                dto = _dataverseMapper.MapToDto(target);

                _logger.WriteLog("Enriching target...");
                dto = _enricher.Execute(dto);

                _logger.WriteLog("Retrieving context specific notification values...");

                var recipients = to.Split(';');
                var preferred = string.Empty;
                var email = string.Empty;

                if (recipients.Length == 1)
                {
                    var outcome = _coreStaffRetriever.Retrieve(to);
                    if (outcome.resolved && outcome.coreStaffMember != null)
                    {
                        preferred = outcome.coreStaffMember.GetAttributeValue<string>(CoreStaffMemberFields.PreferredName) ?? fallbackName;
                    }
                }
                
                _logger.WriteLog("Creating HTML object...");

                var newId = _notificationMapper.CreateNotification(
                    dto, 
                    type, 
                    to, 
                    preferred, 
                    cc ?? "",
                    customTags
                );

                _logger.WriteLog($"Mapped to NotificationRequest and new Entity created with id: {newId}");
                return newId;
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex, "Exception: ");
                throw;
            }
        }
        public List<Guid> BuildDigestEmailNotification(
            string targetEntity,
            OperationType type,
            ColumnSet columns,
            IReadOnlyDictionary<string, string?>? customTags = null
        )
        {
            var newIds = new List<Guid>();
            var digests = _digestQueryEngine.GetDigests(
                $"Notification Digest for {targetEntity}",
                "Custos Overview",
                targetEntity,
                columns
            );

            foreach (var element in digests)
            {
                var metricsTable = element.digest.BuildMetricsTable(_styleProvider);
                
                newIds.Add(
                    _notificationMapper.CreateSummaryNotification(
                        element.digest.Records,
                        OperationType.SummaryNotification,
                        element.to,
                        element.digest.Name,
                        "my.workflows@stpauls.nsw.edu.au",
                        metricsTable,
                        customTags
                    )
                );
            }

            return newIds;
        }
        
    }
}