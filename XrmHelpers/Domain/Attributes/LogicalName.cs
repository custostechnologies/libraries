using System;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Attributes
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