using System;
using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;

namespace spgsWebTools.XrmHelpers
{
    public class DataverseLogger : ICustomLogger
    {
        private readonly ITracingService _tracer;
        private readonly string _className;
        public DataverseLogger(
            ITracingService tracer,
            string className
        )
        {
            _tracer = tracer;
            _className = className;
        }
        public void WriteLog()
        {
            _tracer.Trace("Logger.WriteLog() called with no arguments.");
        }
        public void WriteLog(string message, [CallerMemberName] string memberName = "")
        {
            _tracer.Trace($"[{_className}.{memberName}] {message}");
        }
    }
}