namespace custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IgnoreOnCreateAttribute : Attribute
    { }
}