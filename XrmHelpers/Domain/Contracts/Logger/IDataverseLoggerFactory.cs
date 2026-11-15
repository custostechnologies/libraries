namespace Custos.XrmHelpers
{
    public interface IDataverseLoggerFactory
    {
        IDataverseLogger Create<T>();
    }
}