namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]

    public class DataverseReferenceAttribute : Attribute
    {
        public string TargetEntity { get; }
        //public bool? IsPolymorphic { get; }
        public DataverseReferenceAttribute(
            string targetEntity//,
            //bool isPolymorphic = false
        )
        {
            TargetEntity = targetEntity;
            //IsPolymorphic = isPolymorphic;
        }
    }
}