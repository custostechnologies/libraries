using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Services
{
    public class HashResult
    {
        public string PlaintextHash { get; set; }
        public int EncodedHash { get; set; }
    }
    public interface IHashService<TDto>
    {
        HashResult Generate(TDto dto);
    }
}