namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IgnoreSrcMapping : Attribute
    { }
}