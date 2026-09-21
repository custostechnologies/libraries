using System;
using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace spgsWebTools.XrmHelpers
{
    public class FunctionLogger : ICustomLogger
    {
        private readonly ILogger<FunctionLogger> _logger;
        public FunctionLogger(
            ILogger<FunctionLogger> logger
        )
        {
            _logger = logger;
        }
        public void WriteLog()
        {
            _logger.LogInformation("Logger.WriteLog() called without args.");
        }
        public void WriteLog(
            string message,
            [CallerMemberName] string memberName = ""
        )
        {
            _logger.LogInformation($"[{memberName}] {message}");
        }
    }
}