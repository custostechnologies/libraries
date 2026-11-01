using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Shared.Utilities
{
    public static class EnumExtensions
    {
        public static OptionSetValue ToOptionSetValue<T>(
            this T value)
            where T : Enum
        {
            return new OptionSetValue(
                Convert.ToInt32(value));
        }

        public static T ToEnum<T>(
            this OptionSetValue value)
            where T : Enum
        {
            return (T)Enum.ToObject(
                typeof(T),
                value.Value);
        }
        public static object ToEnum(
            this OptionSetValue value,
            Type enumType
        )
        {
            return Enum.ToObject(enumType, value.Value);
        }
    }
}