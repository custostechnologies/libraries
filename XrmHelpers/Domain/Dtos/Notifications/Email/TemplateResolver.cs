using System.Text;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public record EmailTemplate(Type DtoType, OperationType Operation, string TemplateKey);
    public class TemplateResolver : ITemplateResolver
    {
        private readonly EmailTemplates _emailTemplateQuery;
        private readonly Themes _themeQuery;
        private readonly TemplateSnippetIntersects _intersectQuery;
        private readonly IDataverseLogger _logger;
        private readonly FileDownloaderService _downloaderService;
        private static readonly List<EmailTemplate> EmailTemplates = new()
        {
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.Create, "newSafetyIncident") },
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.UpdateOwner, "updateIncidentOwner") },
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.CloseRecord, "closeSafetyIncident") },
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.SummaryNotification, "safetyIncidentDigest") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.Create, "newChildProtectionConcern") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.UpdateOwner, "updateConcernOwner") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.UpdateOwner, "closeChildProtectionConcern") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.SummaryNotification, "childProtectionConcernDigest") },
            { new EmailTemplate(typeof(TaskDto), OperationType.UpdateOwner, "newTaskAssigned") },
            { new EmailTemplate(typeof(TaskDto), OperationType.SummaryNotification, "taskDigest") },
            { new EmailTemplate(typeof(WorkersCompensationClaimDto), OperationType.UpdateOwner, "newWorkersCompensationClaimAssigned") },
        };

        // mappers
        private IDataverseMapper<EmailTemplateDto> _emailTemplateMapper;
        private IDataverseMapper<ThemeDto> _themeMapper;
        private IDataverseMapper<HTMLSnippetDto> _htmlSnippetMapper;
        private IDataverseMapper<TemplateSnippetIntersectDto> _intersectMapper;
        public TemplateResolver(
            EmailTemplates emailTemplateQuery,
            Themes themeQuery,
            TemplateSnippetIntersects intersectQuery,
            IDataverseLoggerFactory loggerFactory,
            FileDownloaderService downloaderService,
            IDataverseMapper<EmailTemplateDto> emailTemplateMapper,
            IDataverseMapper<ThemeDto> themeMapper,
            IDataverseMapper<HTMLSnippetDto> htmlSnippetMapper,
            IDataverseMapper<TemplateSnippetIntersectDto> intersectMapper
        )
        {
            _emailTemplateQuery = emailTemplateQuery;
            _themeQuery = themeQuery;
            _intersectQuery = intersectQuery;
            _downloaderService = downloaderService;
            _emailTemplateMapper = emailTemplateMapper;
            _themeMapper = themeMapper;
            _htmlSnippetMapper = htmlSnippetMapper;
            _intersectMapper = intersectMapper;

            _logger = loggerFactory.Create<TemplateResolver>();
        }
        public NotificationTemplate Resolve(Type dtoType, OperationType operationType)
        {
            EmailTemplateDto? _emailTemplateDto = null;
            ThemeDto? _themeDto = null;
            List<TemplateSnippetIntersectDto>? _intersectDtos;

            NotificationRequestDto requestDto = new NotificationRequestDto();

            EntityCollection? _intersects;
            Entity? _emailTemplate;
            Entity? _theme;
            byte[] _htmlTemplateBytes;
            string _contentCardHtmlString = string.Empty;
            string _inlineTableHtmlString = string.Empty;
            string _singleButtonHtmlString = string.Empty;

            string templateKey = string.Empty;
            string htmlContent = string.Empty;

            // retrieve email template
            _logger.WriteLog("Retrieving email template...");
            
            var template = EmailTemplates
                .Find(
                    x => x.DtoType == dtoType && x.Operation == operationType
                );
            
            if (template == null)
            {
                throw new InvalidPluginExecutionException(
                    $"No template configured for {dtoType.Name}"
                );
            } else
            {
                templateKey = template.TemplateKey;
            }
            
            _logger.WriteLog($"TemplateKey resolved to: {templateKey}");

            // inject tags values based on incoming dto (regarding obj)
            _logger.WriteLog("Using _tagResolverEngine to inject dto values into tag list.");
            
            if (!string.IsNullOrWhiteSpace(templateKey))
            {
                _logger.WriteLog("Retrieving emailTemplateDto...");

                _emailTemplate = _emailTemplateQuery.GetEntityByKey(templateKey);

                if (_emailTemplate == null)
                {
                    throw new Exception("Email template returned null.");
                }
                else
                {
                    _logger.WriteLog("Creating _emailTemplateDto...");

                    _emailTemplateDto = _emailTemplateMapper.MapToDto(_emailTemplate);
                    // Retrieve HTML Template
                    _logger.WriteLog($"Email template retrieved. Downloading HTML Template file attachment.");

                    requestDto.RequestTitle = _emailTemplateDto.Name ?? "";
                    requestDto.EmailTemplate = new EntityReference(
                        EntityLogicalNames.EmailTemplate,
                        _emailTemplate.Id
                    );
                    
                    // ARGS TO NOTIFICATION PIPELINE CALL

                    if (string.IsNullOrWhiteSpace(requestDto.CCString)) requestDto.CCString = _emailTemplateDto.CC;
                    //requestDto.RegardingRecordId = dto.EntityId;
                    //requestDto.CCString = _emailTemplateDto.CC;
                    
                    requestDto.Importance = 0;
                    requestDto.Subject = _emailTemplateDto.Subject;
                    requestDto.SendFrom = _emailTemplateDto.SendFrom;
                    requestDto.NotificationType = Enums.NotificationType.Email;

                    _htmlTemplateBytes = _downloaderService.Execute(
                        new EntityReference(EntityLogicalNames.EmailTemplate, _emailTemplate.Id),
                        EmailTemplateFields.HtmlTemplate
                    );

                    htmlContent = Encoding.UTF8.GetString(_htmlTemplateBytes);

                    _logger.WriteLog($"HTML template content retrieved...");
                }
            }

            #region /* RETRIEVE THEME */
            _logger.WriteLog($"Retrieving theme...");
            if (_emailTemplateDto != null && _emailTemplateDto.Theme != null)
            {
                _logger.WriteLog($"_emailTemplateDto.Theme.Id: {_emailTemplateDto.Theme.Id}");

                _logger.WriteLog($"Retrieving theme");
                _theme = _themeQuery.GetThemeByRef(
                    new EntityReference(
                        EntityLogicalNames.Theme,
                        _emailTemplateDto.Theme.Id
                    )
                );

                if (_theme != null) _themeDto = _themeMapper.MapToDto(_theme);
            }
            #endregion

            #region /* RETRIEVE TEMPLATE-SNIPPET INTERSECTS */
            _logger.WriteLog($"Retrieving intersects.");
            if (_emailTemplateDto != null)
            {
                _intersects = _intersectQuery.GetIntersectsByTemplateRef(
                    new EntityReference(
                        EntityLogicalNames.EmailTemplate,
                        _emailTemplateDto.EmailTemplateId ?? Guid.Empty
                    )
                );

                _intersectDtos = new List<TemplateSnippetIntersectDto>();

                if (_intersects.Entities.Count > 0)
                {
                    foreach (Entity entity in _intersects.Entities)
                    {
                        var intersectDto = _intersectMapper.MapToDto(entity);

                        if (intersectDto != null) _intersectDtos.Add(intersectDto);

                        var snippetRef = intersectDto != null
                            ? intersectDto.RelatedHTMLSnippet
                            : null;

                        if (snippetRef == null)
                        {
                            _logger.WriteLog("snippetRef is null, continue.");
                            continue;
                        }

                        byte[] snippetBytes = _downloaderService.Execute(
                                snippetRef,
                                HTMLSnippetFields.HTMLFile
                            );

                        if (intersectDto == null)
                        {
                            continue;
                        }
                        else if (intersectDto.Name.Contains("Content Card"))
                        {
                            _contentCardHtmlString = Encoding.UTF8.GetString(snippetBytes);
                        }
                        else if (intersectDto.Name.Contains("Table"))
                        {
                            _inlineTableHtmlString = Encoding.UTF8.GetString(snippetBytes);
                        }
                        if (intersectDto.Name.Contains("Single Button"))
                        {
                            _singleButtonHtmlString = Encoding.UTF8.GetString(snippetBytes);
                        }
                    }
                }
            }
            #endregion
            
            return new NotificationTemplate()
            {
                EmailTemplate = _emailTemplateDto,
                Theme = _themeDto,
                HtmlTemplate = htmlContent,
                ContentCardHtml = _contentCardHtmlString,
                TableHtml = _inlineTableHtmlString,
                ButtonHtml = _singleButtonHtmlString
            };
        }
    }
}