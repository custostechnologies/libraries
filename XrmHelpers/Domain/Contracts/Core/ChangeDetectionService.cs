using System.Reflection;
using Custos.XrmHelpers;
using Custos.XrmHelpers.Infrastructure.Dataverse;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class ChangeDetectionService<TDto> : IChangeDetectionService<TDto> where TDto : IDto
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        private readonly IHashService<TDto> _hashService;
        public ChangeDetectionService(
            IDataverseLoggerFactory loggerFactory,
            IDataverseContext context, 
            IHashService<TDto> hashService
        )
        {
            _context = context;
            _hashService = hashService;
            _logger = loggerFactory.Create<ChangeDetectionService<TDto>>();
        }
        public DeltaComparison<TDto> Compare(TDto prev, TDto next)
        {
            _logger.WriteLog($"Comparing old and new DTO's.");

            var oldHash = _hashService.Generate(prev);
            _logger.WriteLog($"oldHash: {oldHash}");

            var newHash = _hashService.Generate(next);
            _logger.WriteLog($"newHash: {newHash}");

            var changes = new List<PropertyInfo>();
            var hashProperties = typeof(TDto)
                .GetProperties()
                .Where(
                    x => x.IsDefined(typeof(HashAttribute))
                        || x.IsDefined(typeof(Attributes.KeyAttribute))
                ).ToArray();

            var forcePlugin = prev.ForcePlugin;
            _logger.WriteLog($"forcePlugin: {forcePlugin}");

            foreach (var property in hashProperties)
            {
                _logger.WriteLog($"property.Name: {property.Name}");

                var oldValue = property.GetValue(prev);
                _logger.WriteLog($"oldValue: {oldValue}");

                var nextValue = property.GetValue(next);
                _logger.WriteLog($"nextValue: {nextValue}");

                if (!Equals(oldValue, nextValue) || forcePlugin != null)
                {
                    _logger.WriteLog($"Adding {property.Name} to ChangedProperties");
                    changes.Add(property);
                }
            }

            _logger.WriteLog($"Returning DeltaComparison result.");
            var comparison = new DeltaComparison<TDto>
            {
                HasChanged = oldHash != newHash,
                ChangedProperties = changes,
                OldRecord = prev,
                NewRecord = next,
                OldPlaintextHash = oldHash.PlaintextHash,
                NewPlaintextHash = newHash.PlaintextHash,
                OldEncodedHash = oldHash.EncodedHash,
                NewEncodedHash = newHash.EncodedHash
            };

            _logger.WriteLog($"HasChanged: {comparison.HasChanged}");
            _logger.WriteLog(
                $"Changed Properties: {string.Join(", ",
                    comparison.ChangedProperties.Select(x => x.Name))}");

            return comparison;
        }
    }
}