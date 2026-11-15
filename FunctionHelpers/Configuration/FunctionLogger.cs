using Microsoft.Extensions.Logging;

namespace custos.FunctionHelpers.Logging
{
    public class FunctionLogger : IDataverseLogger
    {
        private readonly ILogger<FunctionLogger> _logger;
        private readonly string? _className;

        public FunctionLogger(
            ILogger<FunctionLogger> logger,
            string? className = null)
        {
            _logger = logger;
            _className = className;
        }

        public void WriteLog(string message)
        {
            _logger.LogInformation(
                "[{Class}] {Message}",
                _className,
                message);
        }
        public void WriteLog(string message, params object?[] args)
        {
            _logger.LogInformation(
                $"{message} - {string.Join(", ", args ?? [])}"
            );
        }

        public void WriteLog(Exception ex, string message)
        {
            _logger.LogError(
                ex,
                "[{Class}] {Message}",
                _className,
                message);
        }
        public void WriteLog(Exception ex, string message, params object?[] args)
        {
            _logger.LogError(
                ex,
                "[{Class}] {Message}",
                _className,
                string.Format(message, args ?? []));
        }
    }
}