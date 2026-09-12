using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Domain.Attributes;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupEnricher<TDto> : ILookupEnricher<TDto> where TDto : IDto
    {
        private readonly IDataverseContext _context;
        private readonly ILookupEngine _lookupEngine;
        private readonly DataverseLogger _logger;
        private readonly ILookupMetadataFactory _metadataFactory;
        public LookupEnricher(
            IDataverseContext context,
            ILookupEngine lookupEngine,
            ILookupMetadataFactory lookupMetadataFactory
        )
        {
            _context = context;
            _logger = new DataverseLogger(_context.Tracer, typeof(LookupEnricher<TDto>).Name);
            _lookupEngine = lookupEngine;
            _metadataFactory = lookupMetadataFactory;
        }
        public TDto ResolveLookups(TDto dto)
        {
            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                DataverseLookupAttribute lookupAttribute = property.GetCustomAttribute<DataverseLookupAttribute>();
                DataverseLinkAttribute linkAttribute = property.GetCustomAttribute<DataverseLinkAttribute>();

                if (lookupAttribute == null || linkAttribute == null) continue;
                var keyProperty = typeof(TDto).GetProperty(lookupAttribute.KeyProperty.Name);
                var keyValue = keyProperty.GetValue(dto);
                var targetAttribute = lookupAttribute.TargetAttribute;
                var targetEntity = lookupAttribute.TargetEntity;
                var srcEntity = dto.SourceEntityLogicalName;
                var srcAttribute = linkAttribute.SourceLogicalName;
                //var keyType = typeof(keyProperty);

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

                var metadata = _metadataFactory.Create(
                    (int)keyValue,
                    metadataRequest
                );

                var luResult = _lookupEngine.Execute(metadata);

                property.SetValue(dto, luResult.Successes.FirstOrDefault());
            }

            return dto;
        }
    }
}