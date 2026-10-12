using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.PluginTelemetry;
using System.Runtime.CompilerServices;

namespace spgsWebTools.XrmHelpers
{
    public class DataverseLogger : IDataverseLogger
    {
        private readonly ILogger _baseLogger;
        private readonly string _className;
        public DataverseLogger(
            ILogger baseLogger,
            string className
        )
        {
            _baseLogger = baseLogger ?? throw new ArgumentNullException(nameof(baseLogger));
            _className = className;
        }
        public DataverseLogger GetLogger<T>()
        {
            return new DataverseLogger(
                _baseLogger,
                typeof(T).Name
            );
        }
        public void WriteLog()
        {
            _baseLogger.LogInformation(
                "Logger.WriteLog() called with no arguments."
            );
        }
        public void WriteLog(string message)
        {
            _baseLogger.LogInformation(
                $"Log: {message}"
            );
        }
        public void WriteLog(Exception ex, string message)
        {
            _baseLogger.LogInformation(
                $"Log: {ex} - {message}"
            );
        }
        public void WriteLog(string message, [CallerMemberName] string memberName = "")
        {
            _baseLogger.LogInformation(
                $"[{_className}.{memberName}] {message}"
            );
        }
        public void WriteLog(string message, params object?[] args)
        {
            _baseLogger.LogInformation(
                $"[{_className}] {message}",
                args ?? []
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