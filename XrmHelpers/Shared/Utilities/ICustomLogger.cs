using System;
using Microsoft.Xrm.Sdk;
using System.Runtime.CompilerServices;

namespace spgsWebTools.XrmHelpers
{
    public interface ICustomLogger
    {
        void WriteLog();
        void WriteLog(
            string message,
            [CallerMemberName] string memberName = ""
        );
    }
}