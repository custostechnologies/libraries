namespace custos.XrmHelpers
{
    public interface IDataverseLoggerFactory
    {
        IDataverseLogger Create<T>();
    }
}