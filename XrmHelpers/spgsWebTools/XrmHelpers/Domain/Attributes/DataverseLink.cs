using System;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataverseLinkAttribute : Attribute
    {
        public string? SourceLogicalName { get; }
        public string? TargetLogicalName { get; }
        public Type? SourceType { get; }
        public Type? TargetType { get; }
        public DataverseLinkAttribute(string? srcName, Type? srcType, string? trgName, Type? trgType)
        {
            SourceLogicalName = srcName;
            SourceType = srcType;
            TargetLogicalName = trgName;
            TargetType = trgType;
        }
    }
}