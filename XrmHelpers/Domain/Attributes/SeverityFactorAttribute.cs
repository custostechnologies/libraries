namespace spgsWebTools.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SeverityFactorAttribute: Attribute
    {
        public int Factor { get; }
        public SeverityFactorAttribute(int factor)
        {
            Factor = factor;
        }
    }
}