using System.Reflection;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DataverseMapper<TDto> : IDataverseMapper<TDto> where TDto : IDto, new()
    {
        private readonly IDataverseContext _context;
        private readonly ILookupEngine _lookupEngine;
        private readonly DataverseLogger _logger;
        public DataverseMapper(
            IDataverseContext context,
            ILookupEngine lookupEngine
        )
        {
            _context = context;
            _logger = new DataverseLogger(_context.Tracer, typeof(DataverseMapper<TDto>).Name);
            _lookupEngine = lookupEngine;
        }
        public Entity MapToSource(TDto dto)
        {
            var entity = new Entity(dto.SourceEntityLogicalName);
            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                var srcName = string.Empty;

                DataverseLinkAttribute link = property.GetCustomAttribute<DataverseLinkAttribute>();

                if (link == null) continue;

                var value = property.GetValue(dto);

                if (value == null) continue;

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
        public Entity MapToTarget(TDto dto)
        {
            var entity = new Entity(dto.TargetEntityLogicalName);
            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                var link = property.GetCustomAttribute<DataverseLinkAttribute>();

                if (link == null) continue;

                var value = property.GetValue(dto);

                if (value == null) continue;

                entity[link.TargetLogicalName] = value;
            }

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
                    var srcName = string.Empty;

                    DataverseLinkAttribute link = property.GetCustomAttribute<DataverseLinkAttribute>();
                    if (link == null) continue;
                    var srcType = Nullable.GetUnderlyingType(link.SourceType) ?? link.SourceType;
                    var dtoValueType =
                        Nullable.GetUnderlyingType(property.PropertyType)
                            ?? property.PropertyType;

                    if (link.SourceLogicalName == null)
                        continue;

                    srcName = link.SourceLogicalName;

                    if (!srcEntity.Contains(srcName)) continue;
                    var value = srcEntity[srcName];

                    if (value == null)
                    {
                        continue;
                    }
                    else
                    {
                        object setValue;
                        if (srcType == dtoValueType)
                        {
                            setValue = value;
                        }
                        else if (property.PropertyType.IsEnum)
                        {
                            // convert string to enum
                            setValue = Enum.Parse(dtoValueType, value.ToString());
                        }
                        else if (dtoValueType == typeof(Guid))
                        {
                            setValue = Guid.Parse(value.ToString());
                        }
                        else if (dtoValueType == typeof(int))
                        {
                            setValue = Convert.ToInt32(value);
                        }
                        else if (dtoValueType == typeof(EntityReference))
                        {
                            continue;
                        }
                        else
                        {
                            setValue = Convert.ChangeType(value, srcType);
                        }

                        property.SetValue(dto, setValue);
                    }
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

            _logger.WriteLog($"Returning DTO to caller...");
            return dto;
        }
    }
}