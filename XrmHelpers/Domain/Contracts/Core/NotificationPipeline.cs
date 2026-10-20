using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;
using spgsWebTools.XrmHelpers.Infrastructure.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
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
            _styleProvider = styleProvider;
            _logger = loggerFactory.Create<NotificationPipeline<TDto>>();
        }
        public Guid BuildEmailNotification(
            Entity target, 
            OperationType type, 
            string to, 
            string? cc, 
            string fallbackName
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
                    var outcome = RetrieveCoreStaffMember(to);
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
                    cc ?? ""
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
            ColumnSet columns
        )
        {
            var newIds = new List<Guid>();
            var digests = _digestQueryEngine.GetDigests(
                $"Notification Digest for {targetEntity}",
                "spgsWebTools Overview",
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
                        element.digest.TeamName,
                        "my.workflows@stpauls.nsw.edu.au",
                        metricsTable
                    )
                );
            }

            return newIds;
        }
        private (
            bool resolved, 
            Entity? coreStaffMember
        ) RetrieveCoreStaffMember(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.WriteLog("RetrieveUserPreferredName called with a null address. Falling back to default.");
                return (false, null);
            }

            var domain = "@stpauls.nsw.edu.au";
            var addr = new System.Net.Mail.MailAddress(email);
            var valid = addr.Address.Equals(email, StringComparison.OrdinalIgnoreCase) && email.EndsWith(domain, StringComparison.OrdinalIgnoreCase);
            
            if (valid)
            {
                Entity staffMember = _coreStaffMemberQuery.GetEntityByOrgEmail(email);

                if (staffMember.Id == Guid.Empty)
                {
                    _logger.WriteLog("Unable to find CoreStaffMember. Returning ");
                    return (false, null);
                }

                _logger.WriteLog($"Resolved CoreStaffMember for address: {email}. Returning true and Entity");
                return (true, staffMember);
            } else
            {
                _logger.WriteLog("Invalid email (non-org). Falling back to default.");
                return (false, null);
            }
        }
    }
}