using System;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DataverseLookupAttribute : Attribute
    {
        public Type KeyProperty { get; }
        public string Target { get; }
        public string TargetAttribute { get; }
        public DataverseLookupAttribute(
            Type keyProperty,
            string targetEntityName,
            string targetAttributeName
        )
        {
            KeyProperty = keyProperty;
            TargetEntity = targetEntityName;
            TargetAttribute = targetAttributeName;
        }
    }
}