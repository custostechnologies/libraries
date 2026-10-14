using System.Reflection;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Resolvers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DataverseMapper<TDto> : IDataverseMapper<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseLogger _logger;
        private readonly ILookupEngine _lookupEngine;
        public DataverseMapper(
            IDataverseLoggerFactory loggerFactory,
            ILookupEngine lookupEngine
        )
        {
            _logger = loggerFactory.Create<DataverseMapper<TDto>>();
            _lookupEngine = lookupEngine;
        }
        public Entity MapToSource(TDto dto)
        {
            var entity = new Entity(dto.SourceEntityLogicalName);
            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                var srcName = string.Empty;

                var ignore = property.GetCustomAttribute<IgnoreOnCreateAttribute>();
                if (ignore != null) continue;

                var link = property.GetCustomAttribute<DataverseLinkAttribute>();
                if (link == null) continue;

                var value = property.GetValue(dto);

                if (value == null) continue;

                if (link.SourceType == null) throw new InvalidOperationException("DataverseLink attribute source type property cannot be NULL when attempting to MapToSource.");

                var srcType = Nullable.GetUnderlyingType(link.SourceType) ?? link.SourceType;
                var dtoValueType = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();

                if (link.SourceLogicalName == null)
                    continue;

                srcName = link.SourceLogicalName;

                if (srcType == dtoValueType)
                {
                    entity[srcName] = value;
                }
                else if (srcType == typeof(OptionSetValue))
                {
                    entity[srcName] = new OptionSetValue(Convert.ToInt32(value));
                }
                else if (srcType == typeof(string) && dtoValueType == typeof(Guid))
                {
                    entity[srcName] = value.ToString() ?? Guid.Empty.ToString();
                }
                else
                {
                    entity[srcName] = Convert.ChangeType(value, srcType);
                }
            }

            return entity;
        }
        public Entity MapToSourceDelta(TDto dto, DeltaComparison<TDto> deltaComparison)
        {
            var entity = new Entity(dto.SourceEntityLogicalName);
            var incomingDto = deltaComparison.NewRecord;
            var deltaProperties = deltaComparison.ChangedProperties;

            foreach (var property in deltaProperties)
            {
                var srcName = string.Empty;

                var hash = property.GetCustomAttribute<HashAttribute>();
                var link = property.GetCustomAttribute<DataverseLinkAttribute>();
                var touchDateTime = property.GetCustomAttribute<TouchDateTimeAttribute>();

                if (hash == null || link == null) continue;
                if (link.SourceType == null) throw new InvalidOperationException("DataverseLink attribute source type property cannot be NULL when attempting to MapToSourceDelta.");

                if (hash.Identifier == "PlaintextHash")
                {
                    entity[link.SourceLogicalName] = deltaComparison.NewPlaintextHash;
                    continue;
                }
                if (hash.Identifier == "EncodedHash")
                {
                    entity[link.SourceLogicalName] = deltaComparison.NewEncodedHash;
                    continue;
                }
                if (touchDateTime != null)
                {
                    entity[link.SourceLogicalName] = DateTime.UtcNow;
                    continue;
                }

                if (link == null) continue;

                var value = property.GetValue(incomingDto);

                if (value == null) continue;

                var srcType = link.SourceType;
                var dtoValueType = value.GetType();

                if (link.SourceLogicalName == null)
                    continue;

                srcName = link.SourceLogicalName;

                if (srcType == dtoValueType)
                {
                    entity[srcName] = value;
                }
                else if (srcType == typeof(OptionSetValue))
                {
                    entity[srcName] = new OptionSetValue(Convert.ToInt32(value));
                }
                else if (srcType == typeof(string) && dtoValueType == typeof(Guid))
                {
                    entity[srcName] = value.ToString() ?? Guid.Empty.ToString();
                }
                else
                {
                    entity[srcName] = Convert.ChangeType(value, srcType);
                }
            }

            return entity;
        }
        public TDto MapJsonToDto(string payload)
        {
            _logger.WriteLog($"Entered MapJsonToDto with typeof(TDto).Name: {typeof(TDto).Name}");

            var dto = new TDto();

            using var document = JsonDocument.Parse(payload);

            foreach (var property in typeof(TDto).GetProperties())
            {
                try
                {
                    var link = property.GetCustomAttribute<DataverseLinkAttribute>();
                    
                    if (link == null)
                        continue;

                    if (string.IsNullOrWhiteSpace(link.SourceLogicalName))
                        continue;

                    if (!document.RootElement.TryGetProperty(
                        link.SourceLogicalName ?? "",
                        out var jsonValue))
                    {
                        continue;
                    }

                    var propertyType =
                        Nullable.GetUnderlyingType(property.PropertyType)
                        ?? property.PropertyType;

                    object? value = ConvertJsonValue(
                        jsonValue,
                        propertyType);

                    property.SetValue(dto, value);

                    _logger.WriteLog($"Set propertyName: {property.Name} of type {propertyType} to {value}");
                }
                catch (Exception ex)
                {
                    _logger.WriteLog(
                        $"Failed mapping property {property.Name}. Exception: {ex}"
                    );

                    throw;
                }
            }

            return dto;
        }
        public Entity MapToTarget(TDto dto)
        {
            _logger.WriteLog($"\nEntered MapToTarget with typeof(Dto).Name: {typeof(TDto).Name}");

            var keyCollection = dto.KeyCollection;

            _logger.WriteLog($"Creating new entity for EntityLogicalName: {dto.TargetEntityLogicalName}");
            if (keyCollection != null) _logger.WriteLog($"Using keyCollection: {keyCollection}");

            var entity = keyCollection != null
                ? new Entity(dto.TargetEntityLogicalName, keyCollection)
                : new Entity(dto.TargetEntityLogicalName);

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

                    if (link.TargetType == null) throw new InvalidOperationException("DataverseLink attribute target type property cannot be NULL when attempting to MapToTarget.");

                    var targetType = link.TargetType;
                    var srcType = link.SourceType;
                    var dtoValueType = property.PropertyType;

                    if (link.TargetLogicalName == null)
                        continue;

                    var targetAttribute = link.TargetLogicalName;

                    if (property.PropertyType.IsEnum)
                    {
                        setValue = setValue = new OptionSetValue(Convert.ToInt32(value));
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
                        //setValue = Guid.Parse(value.ToString());
                        setValue = Guid.TryParse(value.ToString(), out Guid result)
                            ? result
                            : Guid.Empty;
                    }
                    else
                    {
                        _logger.WriteLog(
                            $"Attempting ChangeType." +
                            $" Property={property.Name}" +
                            $" ValueType={value.GetType().FullName}" +
                            $" TargetType={targetType.FullName}"
                        );
                        setValue = Convert.ChangeType(value, targetType);
                    }

                    entity[link.TargetLogicalName] = setValue;
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
            return entity;
        }
        public TDto MapToDto(Entity srcEntity)
        {
            _logger.WriteLog($"Entered MapToDto with typeof(Dto).Name: {typeof(TDto).Name}");

            var dto = new TDto();
            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                try
                {
                    if (property == null) continue;

                    DataverseLinkAttribute? link = property.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null) continue;

                    if (string.IsNullOrWhiteSpace(link.SourceLogicalName)) continue;

                    var srcName = link.SourceLogicalName;

                    if (!srcEntity.Contains(srcName)) continue;

                    var value = NormaliseAlias(srcEntity[srcName]);

                    if (value == null) continue;

                    var dtoValueType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    _logger.WriteLog(
                        $"Property={property.Name} " +
                        $"SrcName={srcName} " +
                        $"DtoType={dtoValueType.Name} " +
                        $"ValueType={value.GetType().Name}"
                    );

                    object setValue;

                    if (value is EntityReference reference)
                    {
                        if (dtoValueType == typeof(Guid)) setValue = reference.Id;

                        else if (dtoValueType == typeof(string)) setValue = reference.Name ?? string.Empty;

                        else if (dtoValueType == typeof(EntityReference)) setValue = reference;

                        else throw new InvalidOperationException($"Can't map EntityReference to {dtoValueType.Name} for property: {property.Name}");
                    }

                    else if (value is OptionSetValue osv && dtoValueType.IsEnum)
                    {
                        var optionValue = osv.Value;
                        switch (dtoValueType.Name)
                        {
                            case "AdmittedToHospital":
                                setValue = (AdmittedToHospital)optionValue;
                                break;
                            case "CustomIncidentStatus":
                                setValue = (CustomIncidentStatus)optionValue;
                                break;
                            case "CustomIncidentStatusReason":
                                setValue = (CustomIncidentStatusReason)optionValue;
                                break;
                            case "IncidentCategory":
                                setValue = (IncidentCategory)optionValue;
                                break;
                            case "IncidentSeverity":
                                setValue = (IncidentSeverity)optionValue;
                                break;
                            case "IncidentTimeframe":
                                setValue = (IncidentTimeframe)optionValue;
                                break;
                            case "IncidentType":
                                setValue = (IncidentType)optionValue;
                                break;
                            case "InjuredPersonType":
                                setValue = (InjuredPersonType)optionValue;
                                break;
                            case "InjuryBodyPart":
                                setValue = (InjuryBodyPart)optionValue;
                                break;
                            case "InjuryType":
                                setValue = (InjuryType)optionValue;
                                break;
                            case "Location":
                                setValue = (Location)optionValue;
                                break;
                            case "RecordSource":
                                setValue = (RecordSource)optionValue;
                                break;
                            case "RelevantSport":
                                setValue = (RelevantSport)optionValue;
                                break;
                            case "ResponsePriority":
                                setValue = (ResponsePriority)optionValue;
                                break;
                            case "SubmitterPosition":
                                setValue = (SubmitterPosition)optionValue;
                                break;
                            case "Campus":
                                setValue = (Campus)optionValue;
                                break;
                            case "Gender":
                                setValue = (Gender)optionValue;
                                break;
                            case "LinkType":
                                setValue = (LinkType)optionValue;
                                break;
                            default:
                                setValue = Enum.Parse(dtoValueType, optionValue.ToString());
                                break;
                        }
                    }
                    else if (dtoValueType.IsEnum)
                    {
                        _logger.WriteLog($"Entered PropertyType.IsEnum with dtoValueType: {dtoValueType.Name}");

                        switch (dtoValueType.Name)
                        {
                            case "Campus":
                                setValue = CampusResolver.ToEnum(value.ToString() ?? "");
                                break;
                            case "House":
                                setValue = HouseResolver.ToEnum(value.ToString() ?? "");
                                break;
                            case "Gender":
                                setValue = GenderResolver.ToEnum(value.ToString() ?? "");
                                break;
                            default:
                                setValue = Enum.Parse(dtoValueType, value.ToString() ?? "");
                                break;
                        }
                    }

                    else if (value is OptionSetValue optionSetValue && dtoValueType == typeof(int))
                    {
                        setValue = optionSetValue.Value;
                    }

                    else if (dtoValueType == typeof(Guid))
                    {
                        if (value is Guid guid) setValue = guid;

                        else if (Guid.TryParse(value.ToString(), out Guid id)) setValue = id;

                        else throw new InvalidOperationException($"Can't map {value} with type {value.GetType().Name} to Guid for property: {property.Name}");
                    }
                    else if (dtoValueType == typeof(int))
                    {
                        setValue = Convert.ToInt32(value);
                    }
                    else if (dtoValueType == typeof(bool)) setValue = Convert.ToBoolean(value);
                    else if (dtoValueType == typeof(string)) setValue = value.ToString() ?? string.Empty;
                    else if (dtoValueType == value.GetType()) setValue = value;
                    else
                    {
                        setValue = Convert.ChangeType(value, dtoValueType);
                    }

                    property.SetValue(dto, setValue);
                }
                catch (Exception ex)
                {
                    _logger.WriteLog($"Exception: {ex}");

                    throw;
                }
            }

            _logger.WriteLog($"Returning DTO to caller...");
            return dto;
        }
        private static object? NormaliseAlias(object? value)
        {
            if (value is AliasedValue alias) value = alias.Value;

            return value;
        }
        private object? ConvertJsonValue(
            JsonElement element,
            Type targetType)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (targetType.IsEnum)
            {
                return Enum.ToObject(
                    targetType,
                    element.GetInt32());
            }

            if (targetType == typeof(string))
            {
                return element.GetString();
            }

            if (targetType == typeof(bool))
            {
                return element.GetBoolean();
            }

            if (targetType == typeof(int))
            {
                return element.GetInt32();
            }

            if (targetType == typeof(long))
            {
                return element.GetInt64();
            }

            if (targetType == typeof(decimal))
            {
                return element.GetDecimal();
            }

            if (targetType == typeof(double))
            {
                return element.GetDouble();
            }

            if (targetType == typeof(Guid))
            {
                return Guid.Parse(element.GetString()!);
            }

            if (targetType == typeof(DateTime))
            {
                return element.GetDateTime();
            }

            if (targetType == typeof(DateTimeOffset))
            {
                return element.GetDateTimeOffset();
            }

            if (targetType == typeof(List<int>))
            {
                return element
                    .EnumerateArray()
                    .Select(x => x.GetInt32())
                    .ToList();
            }

            if (targetType.IsGenericType &&
                targetType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var itemType = targetType.GetGenericArguments()[0];

                var list = (System.Collections.IList)
                    Activator.CreateInstance(targetType)!;

                foreach (var item in element.EnumerateArray())
                {
                    list.Add(
                        ConvertJsonValue(item, itemType)
                    );
                }

                return list;
            }

            return JsonSerializer.Deserialize(
                element.GetRawText(),
                targetType);
        }
    }
}