using System.ComponentModel;

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
        public static string ToDescription(
            this OptionSetValue value, 
            Type enumType
        )
        {
            var enumValue = (Enum)Enum.ToObject(enumType, value);
            return enumValue.GetDescription();
        }
        public static string ToDescription(
            this int value,
            Type enumType
        )
        {
            var enumValue = (Enum)Enum.ToObject(enumType, value);
            return enumValue.GetDescription();
        }
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = field?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
                
            return attribute?.Description ?? value.ToString();
        }
    }
}