using System;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]

    public class DataverseLookupAttribute : Attribute
    {
        public string KeyProperty { get; }
        public string TargetEntity { get; }
        public string TargetAttribute { get; }
        public DataverseLookupAttribute(
            string? keyProperty,
            string targetEntity,
            string? targetAttribute
        )
        {
            KeyProperty = keyProperty ?? "";
            TargetEntity = targetEntity;
            TargetAttribute = targetAttribute ?? "";
        }
    }
}