namespace spgsWebTools.XrmHelpers
{
    public interface IDataverseLoggerFactory
    {
        IDataverseLogger Create<T>();
    }
}