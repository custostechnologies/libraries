using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace custos.FunctionHelpers.Logging
{
    public interface IFxCustomLogger : ILogger
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