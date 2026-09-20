using System.Reflection;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Resolvers;
using spgsWebTools.XrmHelpers.Constants;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Schemas;
using System.Text.Json;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;
using System.Text;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Queries;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class NotificationMapper<TDto> : INotificationMapper<TDto> where TDto : INotifiableDto, new()
    {
        private static readonly Dictionary<Type, string> TemplateKeys = new()
        {
            { typeof(SafetyIncidentDto), "newSafetyIncident" }
        };
        private readonly IDataverseContext _context;
        private readonly ILookupEngine _lookupEngine;
        private EmailTemplates _emailTemplateQuery;
        private Themes _themeQuery;
        private TemplateSnippetIntersects _intersectQuery;

        private DataverseMapper<EmailTemplateDto> _emailTemplateMapper;
        private DataverseMapper<ThemeDto> _themeMapper;
        private DataverseMapper<HTMLSnippetDto> _htmlSnippetMapper;
        private DataverseMapper<TemplateSnippetIntersectDto> _intersectMapper;

        private FileDownloaderService _downloaderService;


        private readonly DataverseLogger _logger;
        public NotificationMapper(
            IDataverseContext context,
            ILookupEngine lookupEngine
        )
        {
            _context = context;
            _logger = new DataverseLogger(_context.Tracer, typeof(NotificationMapper<TDto>).Name);
            _lookupEngine = lookupEngine;

            _emailTemplateQuery = new EmailTemplates(_context);
            _themeQuery = new Themes(_context);
            _intersectQuery = new TemplateSnippetIntersects(_context);

            _downloaderService = new FileDownloaderService(_context);

            _emailTemplateMapper = new DataverseMapper<EmailTemplateDto>(_context, _lookupEngine);
            _themeMapper = new DataverseMapper<ThemeDto>(_context, _lookupEngine);
            _htmlSnippetMapper = new DataverseMapper<HTMLSnippetDto>(_context, _lookupEngine);
            _intersectMapper = new DataverseMapper<TemplateSnippetIntersectDto>(_context, _lookupEngine);
        }
        public (string, NotificationRequestDto) CreateHtmlObject(TDto dto)
        {
            _logger.WriteLog("Entered CreateHtmlObject...");

            string templateKey = string.Empty;
            string htmlContent = string.Empty;

            InjectionTagResolverEngine _tagResolverEngine = new InjectionTagResolverEngine();

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

            List<IInjectionTag> tags = new EmailInjectionTags().Tags;
            _logger.WriteLog($"Constructed tags list, tags.Count: {tags.Count}");

            // retrieve email template
            _logger.WriteLog("Retrieving email template...");

            if (!TemplateKeys.TryGetValue(dto.GetType(), out templateKey))
            {
                throw new InvalidPluginExecutionException(
                    $"No template configured for {dto.GetType().Name}"
                );
            }

            _logger.WriteLog($"TemplateKey: {templateKey}");

            // inject tags values based on incoming dto (regarding obj)
            _logger.WriteLog("Using _tagResolverEngine to inject dto values into tag list.");

            _tagResolverEngine.PopulateDataverseTags(
                dto,
                tags
            );

            _logger.WriteLog($"Generating record link from dto class with: dto.SourceEntityLogicalName");

            var recordLink = dto.LinkToRecord(
                _context,
                dto.SourceEntityLogicalName,
                dto.EntityId,
                dto.TargetAppDisplayName
            );

            _tagResolverEngine.PopulateTag(
                dto, tags, "inButtonTargetUrl", recordLink ?? "https://www.google.com"
            );

            var linkText =
                dto.ContextOperationType == OperationType.Create
                ? "Help Me"
                : $"Open in {dto.TargetAppDisplayName}";

            _tagResolverEngine.PopulateTag(
                dto, tags, "inButtonText", linkText
            );

            _tagResolverEngine.PopulateTag(
                dto, tags, "inReceiverFirstName",
                "IN DEV."
            );

            _tagResolverEngine.PopulateTag(
                dto, tags, "inFooterImageUrl",
                "https://spgswebresources.blob.core.windows.net/assets/crest_main_20260217.png"
            );

            _tagResolverEngine.PopulateTag(
                dto, tags, "inHeaderLogoImageUrl",
                "https://spgswebresources.blob.core.windows.net/assets/spgsWebTools_logo_noName_72ppi.png"
            );

            _tagResolverEngine.PopulateTag(
                dto, tags, "inCorrelationId",
                _context.ExecutionContext.CorrelationId.ToString()
            );

            _tagResolverEngine.PopulateTag(
                dto,
                tags,
                "inTableHtmlString",
                dto.GenerateSummaryTable(
                    _context,
                    dto.SourceEntityLogicalName,
                    dto.EntityId,
                    dto.TargetAppDisplayName
                )
            );

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
                    requestDto.RegardingRecordId = dto.EntityId;
                    requestDto.CCString = _emailTemplateDto.CC;
                    requestDto.Importance = 0;
                    requestDto.Subject = _emailTemplateDto.Subject;
                    requestDto.SendFrom = _emailTemplateDto.SendFrom;
                    requestDto.NotificationType = NotificationType.Email;

                    _htmlTemplateBytes = _downloaderService.Execute(
                        new EntityReference(EntityLogicalNames.EmailTemplate, _emailTemplate.Id),
                        EmailTemplateFields.HtmlTemplate
                    );

                    htmlContent = Encoding.UTF8.GetString(_htmlTemplateBytes);

                    _logger.WriteLog($"HTML template content retrieved...");
                }
            }

            // populate tags from emailTemplate
            if (_emailTemplateDto != null)
            {
                _tagResolverEngine.PopulateDataverseTags(
                    _emailTemplateDto,
                    tags
                );
            }

            // Retrieve Theme
            _logger.WriteLog($"Retrieving theme...");
            if (_emailTemplateDto != null && _emailTemplateDto.Theme != null)
            {
                _logger.WriteLog($"_emailTemplateDto.Theme: {JsonSerializer.Serialize(
                        _emailTemplateDto.Theme,
                        new JsonSerializerOptions { WriteIndented = true }
                    )}");

                _logger.WriteLog($"Retrieving theme");
                _theme = _themeQuery.GetThemeByRef(
                    new EntityReference(
                        EntityLogicalNames.Theme,
                        _emailTemplateDto.Theme.Id
                    )
                );

                if (_theme != null) _themeDto = _themeMapper.MapToDto(_theme);

                // populate tags from emailTemplate
                if (_themeDto != null)
                {
                    _tagResolverEngine.PopulateDataverseTags(
                        _themeDto,
                        tags
                    );
                }
            }

            // retrieve template-snippet intersects
            _logger.WriteLog($"Retrieving intersects.");
            if (_emailTemplateDto != null)
            {
                _intersects = _intersectQuery.GetIntersectsByTemplateRef(
                    new EntityReference(
                        EntityLogicalNames.EmailTemplate,
                        _emailTemplateDto.EmailTemplateId ?? Guid.Empty
                    )
                );

                _logger.WriteLog($"_intersects: {JsonSerializer.Serialize(
                        _intersects,
                        new JsonSerializerOptions { WriteIndented = true }
                    )}");

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

            var contentCardStringBuilder = new StringBuilder(_contentCardHtmlString);

            var inlineTableStringBuilder = new StringBuilder(_inlineTableHtmlString);

            var singleButtonStringBuilder = new StringBuilder(_singleButtonHtmlString);

            foreach (var tag in tags)
            {
                if (tag.Value == null) continue;

                var adjustedTag = string.Concat(
                    "{{",
                    tag.InjectionToken,
                    "}}"
                );

                contentCardStringBuilder.Replace(adjustedTag, tag.Value.ToString());
                inlineTableStringBuilder.Replace(adjustedTag, tag.Value.ToString());
                singleButtonStringBuilder.Replace(adjustedTag, tag.Value.ToString());
            }

            var emailContentHtml = $@"
                {contentCardStringBuilder}
                {inlineTableStringBuilder}
                {singleButtonStringBuilder}
            ";

            _tagResolverEngine.PopulateTag(
                dto, tags, "inEmailContentHtml",
                emailContentHtml
            );

            _tagResolverEngine.PopulateTag(
                dto, tags, "inEnvId", _context.ExecutionContext.OrganizationId.ToString()
            );

            var stringBuilder = new StringBuilder(htmlContent);

            foreach (var tag in tags)
            {
                if (tag.Value == null) continue;
                _logger.WriteLog($"Replacing {tag.InjectionToken} with {tag.Value}");
                var adjustedTag = string.Concat(
                    "{{",
                    tag.InjectionToken,
                    "}}"
                );

                stringBuilder.Replace(adjustedTag, tag.Value.ToString());
            }

            return (stringBuilder.ToString(), requestDto);
        }
        public Guid MapToNotificationRequest(NotificationRequestDto dto, string htmlContent)
        {
            Entity notificationRequest = new Entity(EntityLogicalNames.NotificationRequest);

            _logger.WriteLog($"\nEntered MapToNotificationRequest with typeof(Dto).Name: {typeof(NotificationRequestDto).Name}");

            var properties = dto.GetType().GetProperties();
            _logger.WriteLog($"Retrieved DTO properties, length: {properties.Length}");

            foreach (var property in properties)
            {
                try
                {
                    object setValue;

                    var link = property.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null) continue;

                    if (string.IsNullOrWhiteSpace(link.TargetLogicalName))
                    {
                        continue;
                    }

                    _logger.WriteLog($"Mapping property: {link.TargetLogicalName}");

                    var value = property.GetValue(dto);

                    if (value == null) continue;

                    if (link.TargetType == null)
                    {
                        _logger.WriteLog($"TargetType null for property {property.Name}");
                        continue;
                    }

                    var targetType = Nullable.GetUnderlyingType(link.TargetType) ?? link.TargetType;
                    var dtoValueType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    if (link.TargetLogicalName == null)
                        continue;

                    if (property.PropertyType.IsEnum)
                    {
                        setValue = new OptionSetValue(Convert.ToInt32(value));
                    }
                    else if (dtoValueType == targetType)
                    {
                        setValue = value;
                    }
                    else if (targetType == typeof(OptionSetValue))
                    {
                        setValue = value;
                    }
                    else if (targetType == typeof(string) && dtoValueType == typeof(Guid))
                    {
                        setValue = value.ToString() ?? Guid.Empty.ToString();
                    }
                    else if (dtoValueType == typeof(string) && targetType == typeof(Guid))
                    {
                        if (!Guid.TryParse(
                            value?.ToString(),
                            out Guid guidValue))
                        {
                            continue;
                        }

                        setValue = guidValue;
                    }
                    else
                    {
                        _logger.WriteLog(
                            $"Attempting ChangeType." +
                            $" Property={property.Name}" +
                            $" ValueType={value.GetType().FullName}" +
                            $" TargetType={targetType!.FullName}"
                        );
                        setValue = Convert.ChangeType(value, targetType);
                    }

                    notificationRequest[link.TargetLogicalName] = setValue;
                }
                catch (Exception ex)
                {
                    _logger.WriteLog($"Exception: {ex}");
                    throw;
                }
            }

            _logger.WriteLog($"Constructed new Entity to be sent to Target after determining operation type...");

            var newId = _context.Service.Create(notificationRequest);

            if (newId == null || newId == Guid.Empty)
            {
                _logger.WriteLog($"Unable to create new notification request entity");
            }
            else
            {
                FileUploaderService _uploadService = new FileUploaderService(_context);

                _uploadService.Execute(
                    htmlContent,
                    new EntityReference(
                        EntityLogicalNames.NotificationRequest,
                        newId
                    ),
                    NotificationRequestFields.HTMLDocument,
                    "HtmlContent.html"
                );
            }

            return newId;
        }
    }
}