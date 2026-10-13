using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

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
        public NotificationPipeline(
            IDataverseContext context,
            IDataverseLoggerFactory loggerFactory,
            IDataverseMapper<TDto> dataverseMapper,
            INotificationMapper<TDto> notificationMapper,
            ILookupEnricher<TDto> enricher,
            IChangeDetectionService<TDto> changeDetector,
            CoreStaffMembers coreStaffMemberQuery
        )
        {
            _context = context;
            _dataverseMapper = dataverseMapper;
            _notificationMapper = notificationMapper;
            _enricher = enricher;
            _changeDetector = changeDetector;
            _coreStaffMemberQuery = coreStaffMemberQuery;
            _logger = loggerFactory.Create<NotificationPipeline<TDto>>();
        }
        public Guid BuildEmailNotification(Entity target, OperationType type, string to, string? cc, string fallbackName)
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
                var output = _notificationMapper.CreateHtmlObject(dto, type, to, preferred);

                NotificationRequestDto requestDto = output.Item2;

                requestDto.RecipientString = to;
                requestDto.CCString = cc;

                _logger.WriteLog("Mapping to NotificationRequest entity...");
                return _notificationMapper.MapToNotificationRequest(output.Item2, output.Item1);
            }
            catch (Exception ex)
            {
                _logger.WriteLog(ex, "Exception: ");
                throw;
            }
        }
        private (bool resolved, Entity? coreStaffMember) RetrieveCoreStaffMember(string email)
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