using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Shared.Hashing;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using System;
using System.Linq;
using System.Reflection;

namespace spgsWebTools.XrmHelpers.Domain.Services
{
    public class HashService<TDto> : IHashService<TDto>
    {
        private readonly IDataverseContext _context;
        private readonly DataverseLogger _logger;
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

            var result = new HashResult();
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

            result.PlaintextHash = plaintextHash;
            result.EncodedHash = encodedHash;

            return result;
        }
    }
}