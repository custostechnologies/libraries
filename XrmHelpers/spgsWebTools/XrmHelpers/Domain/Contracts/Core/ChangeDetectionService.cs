using System.Reflection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class ChangeDetectionService<TDto> : IChangeDetectionService<TDto>
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        private readonly IHashService<TDto> _hashService;
        public ChangeDetectionService(IDataverseContext context, IHashService<TDto> hashService)
        {
            _context = context;
            _hashService = hashService;
            _logger = new DataverseLogger(
                _context.Tracer,
                typeof(ChangeDetectionService<TDto>).Name
            );
        }
        public DeltaComparison<TDto> Compare(TDto prev, TDto next)
        {
            _logger.WriteLog($"Comparing old and new DTO's.");

            var oldHash = _hashService.Generate(prev);
            var newHash = _hashService.Generate(next);

            var changes = new List<PropertyInfo>();
            var hashProperties = typeof(TDto)
                .GetProperties()
                .Where(x => x.IsDefined(typeof(HashAttribute))
                    || x.IsDefined(typeof(Attributes.KeyAttribute))).ToArray();

            foreach (var property in hashProperties)
            {
                var oldValue = property.GetValue(prev);
                var nextValue = property.GetValue(next);

                if (!Equals(oldValue, nextValue)) changes.Add(property);
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