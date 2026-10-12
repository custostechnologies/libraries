using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace spgsWebTools.XrmHelpers
{
    public class DataverseLoggerFactory : IDataverseLoggerFactory
    {
        private readonly ILogger _baseLogger;

        public DataverseLoggerFactory(ILogger baseLogger)
        {
            _baseLogger = baseLogger
                ?? throw new ArgumentNullException(nameof(baseLogger));
        }

        public IDataverseLogger Create<T>()
        {
            return new DataverseLogger(
                _baseLogger,
                typeof(T).Name);
        }

        public IDataverseLogger Create(string className)
        {
            return new DataverseLogger(
                _baseLogger,
                className);
        }
    }
}