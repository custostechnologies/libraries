using System;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]

    public class DataverseReferenceAttribute : Attribute
    {
        public string TargetEntity { get; }
        public DataverseReferenceAttribute(
            string targetEntity
        )
        {
            TargetEntity = targetEntity;
        }
    }
}