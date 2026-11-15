using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Domain.Services
{
    public class HashResult
    {
        public required string PlaintextHash { get; set; }
        public int EncodedHash { get; set; }
    }
    public interface IHashService<TDto>
    {
        HashResult Generate(TDto dto);
    }
}