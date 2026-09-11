namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IMapper<in T, out K>
    {
        K Map(T source, int resolveId);
    }
}