using System.Runtime.CompilerServices;
using Microsoft.Xrm.Sdk.PluginTelemetry;

namespace spgsWebTools.XrmHelpers
{
    public interface IPluginCustomLogger
    {
        void WriteLog();
        void WriteLog(
            string message,
            [CallerMemberName] string memberName = ""
        );
        void WriteLog(
            Exception ex,
            string message,
            params object?[] args
        );
    }
}