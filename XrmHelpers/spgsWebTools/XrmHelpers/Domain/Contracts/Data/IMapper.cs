namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    // IN:
    // OUT: EntityReference
    public interface IMapper<in T, out K>
    {
        K Map(T source, int resolveId);
    }
}