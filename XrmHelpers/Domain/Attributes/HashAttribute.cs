using System;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class HashAttribute : Attribute
    {
        public string? Identifier { get; }
        public HashAttribute(string? identifier = "") { Identifier = identifier; }
    }
}