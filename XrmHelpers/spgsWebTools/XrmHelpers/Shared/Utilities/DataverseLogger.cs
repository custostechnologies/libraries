using System;
using System.Runtime.CompilerServices;

namespace spgsWebTools.XrmHelpers
{
    public class DataverseLogger(ITracingService tracer, Type targetType)
    {
        private readonly ITracingService _tracer = tracer;
        private readonly Type _targetType = targetType;
        public void WriteLog()
        {
            _tracer.Trace("Logger.WriteLog() called with no arguments.");
        }
        private void WriteLog(string message, [CallerMemberName] string memberName = "")
        {
            _tracer.Trace($"[{_targetType.Name}.{memberName}] {message}");
        }
    }
}