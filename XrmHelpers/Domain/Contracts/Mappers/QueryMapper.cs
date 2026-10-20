using System.Reflection;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Resolvers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class QueryMapper<TDto> : IQueryMapper<TDto> where TDto : IQueryDto, new()
    {
        private readonly IDataverseLogger _logger;
        private readonly ILookupEngine _lookupEngine;
        public QueryMapper(
            IDataverseLoggerFactory loggerFactory,
            ILookupEngine lookupEngine
        )
        {
            _logger = loggerFactory.Create<QueryMapper<TDto>>();
            _lookupEngine = lookupEngine;
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
                            case "PayClaimFormStatusReason":
                                setValue = (PayClaimFormStatusReason)optionValue;
                                break;
                            case "StudentType":
                                setValue = (StudentType)optionValue;
                                break;
                            case "StudentGender":
                                setValue = (StudentGender)optionValue;
                                break;
                            case "ConcernType":
                                setValue = (ConcernType)optionValue;
                                break;
                            case "ConcernFlag":
                                setValue = (ConcernFlag)optionValue;
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
                            case "Status":
                                setValue = StatusResolver.ToEnum(value.ToString() ?? "");
                                break;
                            case "StatusReason":
                                setValue = StatusReasonResolver.ToEnum(value.ToString() ?? "");
                                break;
                            case "PayClaimFormStatusReason":
                                setValue = PayClaimFormStatusReasonResolver.ToEnum(value.ToString() ?? "");
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

                    else if (value is OptionSetValue opsetvalue && dtoValueType == typeof(string))
                    {
                        if (property.Name == "LinkTypeName")
                        {
                            setValue = LinkTypeResolver.ToLabel(opsetvalue.Value);
                        }
                        else if (property.Name == "PayClaimFormStatusReasonLabel")
                        {
                            setValue = PayClaimFormStatusReasonResolver.ToLabel(opsetvalue.Value);
                        }
                        else if (property.Name == "RespondentTypeLabel")
                        {
                            setValue = RespondentTypeResolver.ToLabel(opsetvalue.Value);
                        }
                        else if (property.Name == "FormTypeLabel")
                        {
                            setValue = FormTypeResolver.ToLabel(opsetvalue.Value);
                        }
                        else if (property.Name == "StatusLabel")
                        {
                            setValue = StatusResolver.ToLabel(opsetvalue.Value);
                        }
                        else
                        {
                            setValue = opsetvalue.Value.ToString();
                        }
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
        public TDto MapJsonToDto(JsonElement json)
        {
            var dto = new TDto();

            foreach (PropertyInfo property in typeof(TDto).GetProperties())
            {
                try
                {
                    var link = property.GetCustomAttribute<DataverseLinkAttribute>();

                    if (link == null)
                        continue;

                    if (string.IsNullOrWhiteSpace(link.SourceLogicalName))
                        continue;

                    if (!json.TryGetProperty(
                        link.SourceLogicalName,
                        out JsonElement jsonValue))
                    {
                        continue;
                    }

                    var targetType =
                        Nullable.GetUnderlyingType(property.PropertyType)
                        ?? property.PropertyType;

                    object? value = ConvertValue(jsonValue, targetType);

                    if (value != null)
                    {
                        property.SetValue(dto, value);
                    }
                }
                catch (Exception ex)
                {
                    _logger.WriteLog(ex,
                        $"Failed mapping property {property.Name}");
                    throw;
                }
            }

            return dto;
        }

        private static object? ConvertValue(
            JsonElement value,
            Type targetType)
        {
            if (value.ValueKind == JsonValueKind.Null)
                return null;

            if (targetType == typeof(string))
                return value.GetString();

            if (targetType == typeof(Guid))
                return Guid.Parse(value.GetString()!);

            if (targetType == typeof(int))
                return value.GetInt32();

            if (targetType == typeof(decimal))
                return value.GetDecimal();

            if (targetType == typeof(double))
                return value.GetDouble();

            if (targetType == typeof(bool))
                return value.GetBoolean();

            if (targetType == typeof(DateTime))
            {
                return value.ValueKind switch
                {
                    JsonValueKind.String =>
                        DateTime.Parse(value.GetString()!),

                    JsonValueKind.Number =>
                        DateTimeOffset
                            .FromUnixTimeMilliseconds(value.GetInt64())
                            .DateTime,

                    _ => null
                };
            }

            if (targetType.IsEnum)
            {
                if (value.ValueKind == JsonValueKind.Number)
                {
                    var enumValue = value.GetInt32();
                    return Enum.ToObject(targetType, enumValue);
                }

                return Enum.Parse(
                    targetType,
                    value.GetString()!,
                    true);
            }

            return JsonSerializer.Deserialize(
                value.GetRawText(),
                targetType
            );
        }
    }
}