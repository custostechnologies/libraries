using custos.XrmHelpers.Domain.Attributes;
using custos.XrmHelpers.Shared.Hashing;
using custos.XrmHelpers.Infrastructure.Dataverse;

namespace custos.XrmHelpers.Domain.Services
{
    public class HashService<TDto> : IHashService<TDto>
    {
        private readonly IDataverseContext _context;
        private readonly IDataverseLogger _logger;
        public HashService(
            IDataverseContext context
        )
        {
            _context = context;
            _logger = _context.GetLogger<HashService<TDto>>();
        }
        public HashResult Generate(TDto dto)
        {
            _logger.WriteLog("Generating HashResult using GetBKDRHashKey.");

            var hashProperties = typeof(TDto).GetProperties()
                .Where(property => Attribute.IsDefined(
                    property, typeof(HashAttribute)
                ))
                .ToArray();

            var plaintextHash = string.Empty;
            List<string> valueArray = new List<string>();

            foreach (var property in hashProperties)
            {
                var value = property.GetValue(dto);
                valueArray.Add(value?.ToString() ?? string.Empty);
            }

            plaintextHash = string.Join("|", valueArray);

            var encodedHash = GetBKDRHashKey.Calculate(plaintextHash);

            return new HashResult()
            {
                PlaintextHash = plaintextHash,
                EncodedHash = encodedHash
            };
        }
    }
}