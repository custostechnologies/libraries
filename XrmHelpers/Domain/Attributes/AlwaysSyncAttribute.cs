using System;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AlwaysSyncAttribute : Attribute
    { }
}