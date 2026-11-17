using System.Text;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse.Queries;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public record EmailTemplate(Type DtoType, OperationType Operation, string TemplateKey);
    public class TemplateResolver : ITemplateResolver
    {
        private readonly EmailTemplates _emailTemplateQuery;
        private readonly Themes _themeQuery;
        private readonly TemplateSnippetIntersects _intersectQuery;
        private readonly HTMLSnippets _snippetQuery;
        private readonly IDataverseLogger _logger;
        private readonly FileDownloaderService _downloaderService;
        private static string _cachedSkeleton = "";
        private static readonly List<EmailTemplate> EmailTemplates = new()
        {
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.Create, "newSafetyIncident") },
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.UpdateOwner, "updateIncidentOwner") },
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.CloseRecord, "closeSafetyIncident") },
            { new EmailTemplate(typeof(SafetyIncidentDto), OperationType.SummaryNotification, "safetyIncidentDigest") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.Create, "newChildProtectionConcern") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.UpdateOwner, "updateConcernOwner") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.CloseRecord, "closeChildProtectionConcern") },
            { new EmailTemplate(typeof(ChildProtectionConcernDto), OperationType.SummaryNotification, "childProtectionConcernDigest") },
            { new EmailTemplate(typeof(TaskDto), OperationType.UpdateOwner, "newTaskAssigned") },
            { new EmailTemplate(typeof(TaskDto), OperationType.UpdateOwner, "newTaskAssigned") },
            { new EmailTemplate(typeof(TaskDto), OperationType.SummaryNotification, "taskDigest") },
            { new EmailTemplate(typeof(WorkersCompensationClaimDto), OperationType.UpdateOwner, "newWorkersCompensationClaimAssigned") },
            { new EmailTemplate(typeof(WorkersCompensationClaimDto), OperationType.Create, "newClaimLodged") },
        };

        // mappers
        private readonly IDataverseMapper<EmailTemplateDto> _emailTemplateMapper;
        private readonly IDataverseMapper<ThemeDto> _themeMapper;
        private readonly IDataverseMapper<HTMLSnippetDto> _htmlSnippetMapper;
        private readonly IDataverseMapper<TemplateSnippetIntersectDto> _intersectMapper;
        public TemplateResolver(
            EmailTemplates emailTemplateQuery,
            Themes themeQuery,
            TemplateSnippetIntersects intersectQuery,
            HTMLSnippets snippetQuery,
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
            _snippetQuery = snippetQuery;
            _downloaderService = downloaderService;
            _emailTemplateMapper = emailTemplateMapper;
            _themeMapper = themeMapper;
            _htmlSnippetMapper = htmlSnippetMapper;
            _intersectMapper = intersectMapper;

            _logger = loggerFactory.Create<TemplateResolver>();
        }
        public NotificationTemplate Resolve(Type dtoType, OperationType operationType)
        {
            _logger.WriteLog($"TemplateResolver.Resolve called with dtoType: {dtoType} and operationType: {operationType}");

            EmailTemplateDto? emailTemplateDto = null;
            ThemeDto? themeDto = null;
            List<TemplateSnippetIntersectDto>? intersectDtos = null;

            NotificationRequestDto requestDto = new NotificationRequestDto();

            EntityCollection? intersectEntities;
            Entity? emailTemplate;
            Entity? theme;
            string contentCardHtmlString = string.Empty;
            string inlineTableHtmlString = string.Empty;
            string singleButtonHtmlString = string.Empty;

            string templateKey = string.Empty;
            string htmlContent = string.Empty;

            // retrieve email template
            _logger.WriteLog("Retrieving email template...");
            
            // TODO: change to Dictionary to prevent LINEAR SEARCH EVER EXECUTION O(n)
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

            // TODO: move to GetTemplate() helper
            if (!string.IsNullOrWhiteSpace(templateKey))
            {
                _logger.WriteLog("Retrieving emailTemplateDto...");

                emailTemplate = _emailTemplateQuery.GetEntityByKey(templateKey);

                if (emailTemplate == null)
                {
                    throw new InvalidPluginExecutionException("Email template returned null.");
                }
                else
                {
                    _logger.WriteLog("Creating emailTemplateDto...");

                    emailTemplateDto = _emailTemplateMapper.MapToDto(emailTemplate);
                    // Retrieve HTML Template

                    _logger.WriteLog($"Email template retrieved. RETRIEVING INTERSECT WITH SEQ = -1 and downloading HTML Template file attachment.");

                    requestDto.RequestTitle = emailTemplateDto.Name ?? "";
                    requestDto.EmailTemplate = new EntityReference(
                        EntityLogicalNames.EmailTemplate,
                        emailTemplate.Id
                    );

                    if (string.IsNullOrWhiteSpace(requestDto.CCString)) requestDto.CCString = emailTemplateDto.CC;

                    requestDto.Importance = 0;
                    requestDto.Subject = emailTemplateDto.Subject;
                    requestDto.SendFrom = emailTemplateDto.SendFrom;
                    requestDto.NotificationType = NotificationType.Email;

                    if (string.IsNullOrWhiteSpace(_cachedSkeleton))
                    {
                        var skeletonRef = _snippetQuery.GetSnippetRefBySeq(-1);
                        if (skeletonRef == null) throw new InvalidPluginExecutionException($"Unable to retrieve skeleton HTMLSnippet.");

                        var skeletonBytes = _downloaderService.Execute(
                            skeletonRef,
                            HTMLSnippetFields.HTMLFile
                        );

                        // TODO: add version attribute to snippet table and print to help with debugging
                        _cachedSkeleton = Encoding.UTF8.GetString(skeletonBytes);
                        htmlContent = _cachedSkeleton;
                    } else
                    {
                        htmlContent = _cachedSkeleton;
                    }
                    _logger.WriteLog($"HTML template skeleton retrieved...");
                }
            }
            
            // TODO: move to GetTheme() helper
            #region /* RETRIEVE THEME */
            _logger.WriteLog($"Retrieving theme...");
            if (emailTemplateDto != null && emailTemplateDto.Theme != null)
            {
                _logger.WriteLog($"emailTemplateDto.Theme.Id: {emailTemplateDto.Theme.Id}");

                _logger.WriteLog($"Retrieving theme");
                theme = _themeQuery.GetThemeByRef(
                    new EntityReference(
                        EntityLogicalNames.Theme,
                        emailTemplateDto.Theme.Id
                    )
                );

                if (theme != null) themeDto = _themeMapper.MapToDto(theme);
            }
            #endregion

            // TODO: move to GetIntersects() helper
            #region /* RETRIEVE TEMPLATE-SNIPPET INTERSECTS */
            _logger.WriteLog($"Retrieving intersects.");
            if (emailTemplateDto != null)
            {
                intersectEntities = _intersectQuery.GetIntersectsByTemplateRef(
                    new EntityReference(
                        EntityLogicalNames.EmailTemplate,
                        emailTemplateDto.EmailTemplateId ?? Guid.Empty
                    )
                );

                intersectDtos = new List<TemplateSnippetIntersectDto>();

                // TODO: replace fragile name matching with OSV/Enum
                if (intersectEntities.Entities.Count > 0)
                {
                    foreach (Entity entity in intersectEntities.Entities)
                    {
                        var intersectDto = _intersectMapper.MapToDto(entity);
                        if (intersectDto == null) continue;

                        var intersectName = intersectDto.Name ?? string.Empty;  
                        
                        intersectDtos.Add(intersectDto);
                        var snippetRef = intersectDto.RelatedHTMLSnippet;

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
                        else if (intersectName.Contains("Content Card", StringComparison.OrdinalIgnoreCase))
                        {
                            contentCardHtmlString = Encoding.UTF8.GetString(snippetBytes);
                        }
                        else if (intersectName.Contains("Table", StringComparison.OrdinalIgnoreCase))
                        {
                            inlineTableHtmlString = Encoding.UTF8.GetString(snippetBytes);
                        }
                        else if (intersectName.Contains("Single Button", StringComparison.OrdinalIgnoreCase))
                        {
                            singleButtonHtmlString = Encoding.UTF8.GetString(snippetBytes);
                        }
                    }
                }
            }
            #endregion
            
            return new NotificationTemplate()
            {
                EmailTemplate = emailTemplateDto,
                Theme = themeDto,
                HtmlTemplate = htmlContent,
                ContentCardHtml = contentCardHtmlString,
                TableHtml = inlineTableHtmlString,
                ButtonHtml = singleButtonHtmlString
            };
        }
    }
}