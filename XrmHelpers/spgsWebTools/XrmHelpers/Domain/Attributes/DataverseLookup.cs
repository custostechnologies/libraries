using System;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataverseLookupAttribute : Attribute
    {
        public Type KeyProperty { get; }
        public string TargetEntity { get; }
        public string TargetAttribute { get; }
        public DataverseLookupAttribute(
            Type keyProperty,
            string targetEntity,
            string targetAttribute
        )
        {
            KeyProperty = keyProperty;
            TargetEntity = targetEntity;
            TargetAttribute = targetAttribute;
        }
    }
}