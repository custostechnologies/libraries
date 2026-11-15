namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EnumTypeAttribute : Attribute
    {
        public Type EnumType { get; }
        public EnumTypeAttribute(Type enumType) 
            { EnumType = enumType; }
    }
}