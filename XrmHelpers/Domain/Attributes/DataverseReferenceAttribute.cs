namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]

    public class DataverseReferenceAttribute : Attribute
    {
        public string TargetEntity { get; }
        public DataverseReferenceAttribute(string targetEntity)
        {
            TargetEntity = targetEntity;
        }
    }
}