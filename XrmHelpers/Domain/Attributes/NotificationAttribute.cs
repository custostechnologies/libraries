using System;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotificationAttribute : Attribute
    {
        /*public string? SourceLogicalName { get; }
        public string? TargetLogicalName { get; }
        public Type? SourceType { get; }
        public Type? TargetType { get; }
        public NotificationAttribute(string? srcName, Type? srcType, string? trgName, Type? trgType)
        {
            SourceLogicalName = srcName;
            SourceType = srcType;
            TargetLogicalName = trgName;
            TargetType = trgType;
        }*/
    }
}