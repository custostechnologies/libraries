using System;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class KeyAttribute : Attribute
    {
        public string KeyType { get; }
        public KeyAttribute(string keyType = "NA") { KeyType = keyType; }
    }
}