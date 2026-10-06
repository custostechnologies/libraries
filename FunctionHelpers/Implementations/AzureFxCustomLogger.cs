using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace spgsWebTools.FunctionHelpers.Logging
{
    public class AzureFxCustomLogger<T> : IFxCustomLogger, ILogger
    {
        private readonly ILogger<T> _baseLogger;
        public AzureFxCustomLogger(
            ILogger<T> baseLogger
        )
        {
            _baseLogger = baseLogger;
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
            _baseLogger.LogInformation(
                "[{Member}] {Message}",
                memberName,
                message
            );
        }
        public void WriteLog(Exception ex, string message, params object?[] args)
        {
            var eventId = new EventId(5000, "UnhandledException");
            _baseLogger.LogError(
                eventId,
                ex,
                message,
                args
            );
        }
    }
}