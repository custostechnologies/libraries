using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Services
{
    public interface HashResult
    {
        string PlaintextHash { get; }
        int EncodedHash { get; }
    }
    public interface IHashService<TDto>
    {
        HashResult Generate(TDto dto);
    }
}