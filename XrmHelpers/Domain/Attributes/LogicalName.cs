using System;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class LogicalNameAttribute: Attribute
    {
        public string LogicalName { get; }
        public LogicalNameAttribute(string name)
        {
            LogicalName = name;
        }
    }
}