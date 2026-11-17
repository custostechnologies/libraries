namespace Custos.XrmHelpers.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumColourAttribute : Attribute
    {
        public string HexCode { get; }
        public EnumColourAttribute(string hexCode) 
            { HexCode = hexCode; }
    }
}