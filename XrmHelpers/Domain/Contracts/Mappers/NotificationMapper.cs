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
    public class NotificationMapper<TDto> : INotificationMapper<TDto> where TDto : IDto, new()
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
        private string htmlContent = string.Empty;
        private string templateKey = string.Empty;

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
        public string CreateHtmlObject(TDto dto)
        {
            EmailTemplateDto? _emailTemplateDto = null;
            ThemeDto? _themeDto = null;
            List<TemplateSnippetIntersectDto>? _intersectDtos = null;

            EntityCollection? _intersects = null;
            Entity? _emailTemplate = null;
            Entity? _theme = null;
            byte[] _htmlTemplateBytes;
            string _contentCardHtmlBytes = string.Empty;
            string _inlineTableHtmlBytes = string.Empty;
            string _singleButtonHtmlBytes = string.Empty;

            // retrieve email template
            if (!TemplateKeys.TryGetValue(dto.GetType(), out templateKey))
            {
                throw new InvalidPluginExecutionException(
                    $"No template configured for {dto.GetType().Name}"
                );
            }

            if (!string.IsNullOrWhiteSpace(templateKey))
            {
                _emailTemplate = _emailTemplateQuery.GetEntityByKey(templateKey);

                if (_emailTemplate == null)
                {
                    throw new Exception("Email template returned null.");
                }
                else
                {
                    _emailTemplateDto = _emailTemplateMapper.MapToDto(_emailTemplate);
                    // Retrieve HTML Template
                    _logger.WriteLog($"Email template retrieved. Downloading HTML Template file attachment.");

                    _htmlTemplateBytes = _downloaderService.Execute(
                        new EntityReference(EntityLogicalNames.EmailTemplate, _emailTemplate.Id),
                        EmailTemplateFields.HtmlTemplate
                    );

                    htmlContent = Encoding.UTF8.GetString(_htmlTemplateBytes);

                    _logger.WriteLog($"HTML template content retrieved...");
                }
            }

            // Retrieve Theme
            _logger.WriteLog($"Retrieving theme.");
            if (_emailTemplateDto != null && _emailTemplateDto.Theme != null)
            {
                _theme = _themeQuery.GetThemeByRef(
                    new EntityReference(
                        EntityLogicalNames.Theme,
                        _emailTemplateDto.Theme.Id
                    )
                );

                if (_theme != null) _themeDto = _themeMapper.MapToDto(_theme);
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
                            _contentCardHtmlBytes = Encoding.UTF8.GetString(snippetBytes);
                        }
                        else if (intersectDto.Name.Contains("Table"))
                        {
                            _inlineTableHtmlBytes = Encoding.UTF8.GetString(snippetBytes);
                        }
                        if (intersectDto.Name.Contains("Single Button"))
                        {
                            _singleButtonHtmlBytes = Encoding.UTF8.GetString(snippetBytes);
                        }
                    }
                }
            }

            _logger.WriteLog($"_emailTemplateDto: {JsonSerializer.Serialize(
                    _emailTemplateDto,
                    new JsonSerializerOptions { WriteIndented = true }
                )}");
            _logger.WriteLog($"_themeDto: {JsonSerializer.Serialize(
                    _themeDto,
                    new JsonSerializerOptions { WriteIndented = true }
                )}");
            _logger.WriteLog($"_contentCardHtmlBytes: {JsonSerializer.Serialize(
                    _contentCardHtmlBytes,
                    new JsonSerializerOptions { WriteIndented = true }
                )}");
            _logger.WriteLog($"_inlineTableHtmlBytes: {JsonSerializer.Serialize(
                    _inlineTableHtmlBytes,
                    new JsonSerializerOptions { WriteIndented = true }
                )}");
            _logger.WriteLog($"_singleButtonHtmlBytes: {JsonSerializer.Serialize(
                _singleButtonHtmlBytes,
                new JsonSerializerOptions { WriteIndented = true }
            )}");
            _logger.WriteLog($"_intersectDtos: {JsonSerializer.Serialize(
                _intersectDtos,
                new JsonSerializerOptions { WriteIndented = true }
            )}");

            return htmlContent;
        }
        public Entity MapToNotificationRequest(TDto dto)
        {
            Entity notificationRequest = new Entity(EntityLogicalNames.NotificationRequest);

            _logger.WriteLog($"\nEntered MapToNotificationRequest with typeof(Dto).Name: {typeof(TDto).Name}");

            _logger.WriteLog($"Creating new entity for EntityLogicalName: {dto.TargetEntityLogicalName}");

            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                try
                {
                    object setValue;

                    var link = property.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null) continue;

                    var value = property.GetValue(dto);

                    if (value == null) continue;

                    var targetType = Nullable.GetUnderlyingType(link.TargetType) ?? link.TargetType;
                    var srcType = Nullable.GetUnderlyingType(link.SourceType) ?? link.SourceType;
                    var dtoValueType = property.PropertyType;

                    if (link.TargetLogicalName == null)
                        continue;

                    var targetAttribute = link.TargetLogicalName;

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
                        setValue = Guid.Parse(value.ToString());
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
                    if (ex.InnerException != null)
                    {
                        _logger.WriteLog($"InnerException: {ex.InnerException}");
                    }
                    throw;
                }
            }

            _logger.WriteLog($"Constructed new Entity to be sent to Target after determining operation type...");

            return notificationRequest;
        }
    }
}