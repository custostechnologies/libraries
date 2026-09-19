using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupEngine : ILookupEngine
    {
        private readonly IServiceProvider _services;
        private readonly IDataverseContext _context;
        private DataverseLogger _logger;
        public LookupEngine(
            IDataverseContext context,
            IServiceProvider services
        )
        {
            _context = context;
            _services = services;
            _logger = new DataverseLogger(_context.Tracer, nameof(LookupEngine));

            _logger.WriteLog("Class instantiated via constructor.");
        }
        public ILookupResult<TKey> Execute<TKey>(ILookupMetadata<TKey> metadata)
        {
            var entityName = metadata.TargetEntityLogicalName;
            _logger.WriteLog($"Execution starting to retrieve a record from {metadata.TargetEntityLogicalName}");

            var resolver = _services
                .GetRequiredService<ILookupResolver<TKey>>();

            if (resolver == null)
            {
                _logger.WriteLog("No lookup resolver is registered for the given metadata");
                throw new InvalidPluginExecutionException();
            }

            _logger.WriteLog($"Resolver found, calling resolver.Resolve to complete lookup resolution...");
            return resolver.Resolve(metadata);
        }
    }
}