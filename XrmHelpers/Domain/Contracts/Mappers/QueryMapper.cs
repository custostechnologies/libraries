using System.Reflection;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Domain.Resolvers;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Domain.Contracts
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
                    else if (value is OptionSetValueCollection options
                        && property.PropertyType.IsArray
                        && property.PropertyType.GetElementType().IsEnum)
                    {
                        var enumType = property.PropertyType.GetElementType();
                        var array = Array.CreateInstance(enumType, options.Count);
                        for (int i = 0; i < options.Count; i++)
                        {
                            array.SetValue(
                                Enum.ToObject(enumType, options[i].Value),
                                i
                            );
                        }

                        setValue = array;
                    }
                    else if (value is OptionSetValue osv && dtoValueType.IsEnum)
                    {
                        setValue = EnumExtensions.ToEnum(osv, dtoValueType);
                    }
                    else if (dtoValueType.IsEnum)
                    {
                        setValue = Enum.Parse(
                            dtoValueType,
                            value.ToString() ?? "",
                            ignoreCase: true
                        );
                    }

                    else if (value is OptionSetValue optionSetValue && dtoValueType == typeof(int))
                    {
                        setValue = optionSetValue.Value;
                    }

                    else if (value is OptionSetValue opsetvalue && dtoValueType == typeof(string))
                    {
                        var enumTypeAttribute = property.GetCustomAttribute<EnumTypeAttribute>();
                        if (enumTypeAttribute != null)
                        {
                            setValue = opsetvalue.ToDescription(enumTypeAttribute.EnumType);
                        }
                        
                        setValue = opsetvalue.ToDescription(dtoValueType);                }

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

                    object? value = ValueConverter.FromJsonValue(jsonValue, targetType);

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
    }
}