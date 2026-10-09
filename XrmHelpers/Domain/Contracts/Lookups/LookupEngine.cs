using Microsoft.Xrm.Sdk;
using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class LookupEngine : ILookupEngine
    {
        private readonly IServiceProvider _provider;
        private readonly IDataverseLogger _logger;
        public LookupEngine(
            IDataverseLogger logger,
            IServiceProvider provider
        )
        {
            _provider = provider;
            _logger = logger;
            _logger.WriteLog("Class instantiated via constructor.");
        }
        public ILookupResult<TKey> Execute<TKey>(ILookupMetadata<TKey> metadata)
        {
            var entityName = metadata.TargetEntityLogicalName;
            _logger.WriteLog($"Execution starting to retrieve a record from {metadata.TargetEntityLogicalName}");

            var resolver = _provider
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