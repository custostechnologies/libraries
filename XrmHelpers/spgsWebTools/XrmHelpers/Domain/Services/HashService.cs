using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Shared.Hashing;
using System;
using System.Linq;
using System.Reflection;

namespace spgsWebTools.XrmHelpers.Domain.Services
{
    public class HashResult
    {
        public string PlaintextHash { get; }
        public int EncodedHash { get; }
    }
    public class HashService<TDto> : IHashService<TDto>
    {
        private IDataverseContext _context;
        private DataverseLogger _logger;
        public HashService(
            IDataverseContext context
        )
        {
            _context = context;
            _logger = new DataverseLogger(
                _context.Tracer,
                typeof(HashService<TDto>).Name
            );
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
            string[] valueArray = new[] { };

            foreach (var property in hashProperties)
            {
                var value = property.GetValue(dto);
                var type = value.GetType();

                valueArray.Add(Convert.ChangeType(value, type));
            }

            plaintextHash = string.Join("|", valueArray);

            var encodedHash = GetBKDRHashKey.Calculate(plaintextHash);

            result.PlaintextHash = plaintextHash;
            result.EncodedHash = encodedHash;

            return result;
        }
    }
}