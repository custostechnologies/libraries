using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Domain.Attributes;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupEnricher<TDto> : ILookupEnricher<TDto>
    {
        private readonly IDataverseContext _context;
        private readonly ILookupEngine _lookupEngine;
        private readonly DataverseLogger _logger;
        public LookupEnricher(
            IDataverseContext context,
            ILookupEngine lookupEngine
        )
        {
            _context = context;
            _logger = new DataverseLogger(_context.Tracer, typeof(LookupEnricher<TDto>).Name);
            _lookupEngine = lookupEngine;
        }
        public TDto ResolveLookups(TDto dto)
        {
            var properties = typeof(TDto).GetProperties();

            foreach (var property in properties)
            {
                DataverseLookupAttribute link = property.GetCustomAttribute<DataverseLookupAttribute>();

                if (link == null) continue;

                metadata = InstantiateLookupMetadata(
                    link.KeyProperty,
                    dto.SourceEntityLogicalName,
                    dto.SourceAttribueLogicalName,
                    link.TargetEntity,
                    link.TargetAttribute,
                    dto[link.KeyProperty.GetValue()]
                );

                var luResult = _lookupEngine.Execute(metadata);

                property.SetValue(link.KeyProperty, luResult.Successes.FirstOrDefault());
            }
        }
        private ILookupMetadata InstantiateLookupMetadata(
            Type keyType,
            string srcEntityName,
            string srcAttributeName,
            string targetEntityName,
            string targetAttributeName,
            object value
        )
        {
            var metadataRequest = new LookupMetadataRequest
            {
                AllowMultipleMatches = true,
                ReturnMultipleReferences = true,
                TargetEntityLogicalName = targetEntityName,
                TargetAttributeLogicalName = targetAttributeName,
                SourceEntityLogicalName = srcEntityName,
                SourceAttributeLogicalName = srcAttributeName,
                Columns = new ColumnSet(
                    $"{targetEntityName}id",
                    targetAttributeName
                ),
                TopCount = 500
            };

            var metadataFactory = new LookupMetadataFactory();
            ILookupMetadata metadata = metadataFactory.Create(
                keyType,
                value,
                metadataRequest
            );
            return metadata;
        }
    }
}