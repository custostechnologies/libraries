using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers
{
    public class PluginLogger : IDataverseLogger
    {
        private readonly ITracingService _tracing;
        private readonly string _className;

        public PluginLogger(
            ITracingService tracing,
            string className)
        {
            _tracing = tracing;
            _className = className;
        }
        public void WriteLog(string message, params object[]? args)
        {
            _tracing.Trace(
                $"{message} - {string.Join(", ", args ?? [])}"
            );
        }

        public void WriteLog(string message)
        {
            _tracing.Trace(
                $"[{_className}] {message}");
        }

        public void WriteLog(Exception ex, string message)
        {
            _tracing.Trace(
                $"[{_className}] {message}");

            _tracing.Trace(
                ex.ToString());
        }

        public void WriteLog(Exception ex, string message, params object[]? args)
        {
            _tracing.Trace(
                ex.ToString(),
                "[{Class}] {Message}",
                _className,
                string.Format(message, args ?? []));
        }
    }
}