using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Custos.XrmHelpers.Domain.Attributes;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class LookupEnricher<TDto> : ILookupEnricher<TDto> where TDto : IDto
    {
        private readonly IDataverseContext _context;
        private readonly ILookupEngine _lookupEngine;
        private readonly IDataverseLogger _logger;
        private readonly ILookupMetadataFactory _metadataFactory;
        public LookupEnricher(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context,
            ILookupEngine lookupEngine,
            ILookupMetadataFactory lookupMetadataFactory
        )
        {
            _context = context;
            _logger = loggerFactory.Create<LookupEnricher<TDto>>();
            _lookupEngine = lookupEngine;
            _metadataFactory = lookupMetadataFactory;
        }
        public TDto Execute(TDto dto)
        {
            _logger.WriteLog($"Enriching DTO lookup fields, typeof(TDTO).Name: {typeof(TDto).Name}");

            var properties = typeof(TDto).GetProperties();

            _logger.WriteLog($"Retrieved TDto properties, entering iteration through properties to find and populate lookup fields.");

            if (_metadataFactory == null)
            {
                throw new InvalidPluginExecutionException("Dependency injection failed for MetadataFactory.");
            }

            foreach (var property in properties)
            {
                DataverseLookupAttribute? lookupAttribute = property.GetCustomAttribute<DataverseLookupAttribute>();
                DataverseLinkAttribute? linkAttribute = property.GetCustomAttribute<DataverseLinkAttribute>();

                if (lookupAttribute == null || linkAttribute == null) continue;
                var keyProperty = typeof(TDto).GetProperty(lookupAttribute.KeyProperty);

                if (keyProperty == null)
                {
                    _logger.WriteLog($"Could not find property '{lookupAttribute.KeyProperty}' on DTO of type '{typeof(TDto).Name}'");
                    throw new InvalidPluginExecutionException();
                }
                else
                {
                    _logger.WriteLog($"Lookup field detected - Name: {keyProperty.Name}, PropertyType: {keyProperty.PropertyType}");
                }

                var keyValue = keyProperty.GetValue(dto);
                _logger.WriteLog($"keyValue: {keyValue}");

                if (keyValue == null)
                {
                    _logger.WriteLog($"keyValue is NULL, skipping...");
                    continue;
                }

                var targetAttribute = lookupAttribute.TargetAttribute;
                _logger.WriteLog($"targetAttribute: {targetAttribute}");

                var targetEntity = lookupAttribute.TargetEntity;
                _logger.WriteLog($"targetEntity: {targetEntity}");

                var srcEntity = dto.SourceEntityLogicalName;
                _logger.WriteLog($"srcEntity: {srcEntity}");

                var srcAttribute = linkAttribute.SourceLogicalName;
                _logger.WriteLog($"srcAttribute: {srcAttribute}");

                if (srcAttribute == null) throw new InvalidPluginExecutionException("Unable to construct LookupMetadataRequest with null srcAttribute name.");

                var keyType = keyValue.GetType().Name;
                _logger.WriteLog($"keyType: {keyType}");

                _logger.WriteLog($"Asking for new MetadataRequest from MetadataRequestFactory.");

                var metadataRequest = new LookupMetadataRequest
                {
                    AllowMultipleMatches = true,
                    ReturnMultipleReferences = true,
                    TargetEntityLogicalName = targetEntity,
                    TargetAttributeLogicalName = targetAttribute,
                    SourceEntityLogicalName = srcEntity,
                    SourceAttributeLogicalName = srcAttribute,
                    Columns = new ColumnSet(
                    $"{targetEntity}id",
                        targetAttribute
                    ),
                    TopCount = 500
                };

                _logger.WriteLog($"_metadataFactory null? {_metadataFactory == null}");
                _logger.WriteLog(
                    $"keyValue={(keyValue == null ? "NULL" : keyValue.ToString())}"
                );
                _logger.WriteLog(
                    $"KeyProperty={keyProperty.Name}, Type={keyProperty.PropertyType}"
                );

                var metadata = _metadataFactory!.Create(
                    keyValue,
                    metadataRequest
                );

                _logger.WriteLog($"Running Execute on LookupEngine with keyValue: {keyValue}, querying TargetEntity: {targetEntity}.");

                var luResult = _lookupEngine.Execute(metadata);

                _logger.WriteLog($"\n{luResult.GenerateDetailedLogSummary()}\n");

                var firstSuccess = luResult.Successes.FirstOrDefault();
                if (firstSuccess == null) throw new InvalidPluginExecutionException("LookupEngine failed to retrieve a result from the success metadata.");
                property.SetValue(dto, firstSuccess.References.FirstOrDefault());
            }

            return dto;
        }
    }
}