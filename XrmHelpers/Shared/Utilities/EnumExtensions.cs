using System.ComponentModel;
using System.Reflection;
using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Domain.Attributes;

namespace Custos.XrmHelpers.Shared.Utilities
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
        public static int GetSeverityFactor(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = field?
                .GetCustomAttributes(typeof(SeverityFactorAttribute), false)
                .Cast<SeverityFactorAttribute>()
                .FirstOrDefault();
                
            return attribute?.Factor ?? 0;
        }
        public static string GetColour(this Enum value)
        {
            var field = value
                .GetType()
                .GetField(value.ToString());

            if (field == null)
            {
                throw new InvalidPluginExecutionException(
                    $"Unable to resolve field '{value}' on enum '{value.GetType().Name}'"
                );
            }
            
            var attribute = field?
                .GetCustomAttributes(
                    typeof(EnumColourAttribute),
                    false)
                .OfType<EnumColourAttribute>()
                .FirstOrDefault();

            if (attribute == null)
            {
                throw new InvalidPluginExecutionException(
                    $"EnumColourAttribute missing for '{value.GetType().Name}.{value}'"
                );
            }
            
            return attribute.HexCode;
        }
        public static string BuildBadgeHtml(this Enum value)
        {
            return $@"
                <span style=""display:inline-block;
                padding:4px 10px;
                border-radius:4px;
                background-color:{value.GetColour()};
                color:#FFFFFF;
                font-weight:600;
                font-size:12px;"">
                    {value.GetDescription()}
                </span>";
        }
    }
}