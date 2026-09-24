using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace spgsWebTools.FunctionHelpers.Logging
{
    public class AzureFxCustomLogger : IFxCustomLogger
    {
        private readonly ILogger _baseLogger;
        private readonly string _className;
        public AzureFxCustomLogger(
            ILogger baseLogger,
            string className
        )
        {
            _baseLogger = baseLogger;
            _className = className;
        }
        #region ILogger Implementation

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            _baseLogger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel) => _baseLogger.IsEnabled(logLevel);

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
            => _baseLogger.BeginScope(state);

        #endregion

        public void WriteLog()
        {
            _baseLogger.LogInformation(
                "Logger.WriteLog() called with no arguments."
            );
        }
        public void WriteLog(string message, [CallerMemberName] string memberName = "")
        {
            var logMessage = $"[{_className}.{memberName}] {message}";
            _baseLogger.LogInformation(
                $"App Log Message: {logMessage}"
            );
        }
    }
}