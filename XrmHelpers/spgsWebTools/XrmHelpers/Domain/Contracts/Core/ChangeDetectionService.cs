using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class ChangeDetectionService<TDto>
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
        public ChangeDetectionService(IDataverseContext context)
        {
            _context = context;
            _logger = new DataverseLogger(
                typeof(ChangeDetectionService<TDto>).Name,
                _context.Tracer
            );
        }
        public DeltaComparison<TDto> Compare(TDto prev, TDto next)
        {
            _logger.WriteLog($"Comparing old and new DTO's.");

            var oldHash = new HashService<TDto>().Generate(prev);
            var newHash = new HashService<TDto>().Generate(next);

            var changes = new List<PropertyInfo>();
            var hashProperties = typeof(TDto).GetProperties()
            .Select(
                property => new
                {
                    Property = property,
                    Hash = property.GetCustomAttribute<HashAttribute>(),
                    Key = property.GetCustomAttribute<KeyAttribute>()
                }
            )
            .Where(x => x.Hash != null || x.Key != null).ToArray();

            foreach (var property in hashProperties)
            {
                var oldValue = item.property.GetValue(prev);
                var nextValue = item.property.GetValue(next);

                if (!Equals(oldValue, nextValue)) changes.Add(item.property.Name);
            }

            _logger.WriteLog($"Returning DeltaComparison result.");
            var comparison = new DeltaComparison<TDto>
            {
                HasChanged = oldHash != newHash,
                ChangedAttributes = changes,
                OldRecord = prev,
                NewRecord = next,
                OldPlaintextHash = oldHash.plaintextHash,
                NewPlaintextHash = newHash.plaintextHash,
                OldEncodedHash = oldHash.encodedHash,
                NewEncodedHash = newHash.encodedHash
            };

            _logger.WriteLog($"HasChanged: {comparison.HasChanged}");
            _logger.WriteLog(
                $"Changed Properties: {string.Join(", ",
                    comparison.ChangedAttributes.Select(x => x.Name))}");

            return comparison;
        }
    }
}