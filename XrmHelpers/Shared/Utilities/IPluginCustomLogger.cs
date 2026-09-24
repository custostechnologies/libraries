using System.Runtime.CompilerServices;

namespace spgsWebTools.XrmHelpers
{
    public interface IPluginCustomLogger
    {
        void WriteLog();
        void WriteLog(
            string message,
            [CallerMemberName] string memberName = ""
        );
    }
}