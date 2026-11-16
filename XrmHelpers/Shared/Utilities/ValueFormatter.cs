using System.Reflection;
using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Attributes;

namespace Custos.XrmHelpers.Shared.Utilities
{
    public static class ValueFormatter
    {
        public static string Format(object? value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToString("dd/MM/yyyy HH:mm");
            }

            if (value is DateTime? && (value as DateTime?).HasValue)
            {
                DateTime val = (DateTime)value;
                return val.ToString("dd/MM/yyyy HH:mm");
            }

            if (value is Array array &&
            array.GetType().GetElementType()?.IsEnum == true)
            {
                return string.Join(
                    ", ",
                    array.Cast<Enum>()
                        .Select(e => e.GetDescription())
                );
            }

            if (value is Enum enumValue)
            {
                return enumValue.GetDescription();
            }

            if (value is bool boolValue)
            {
                return boolValue ? "Yes" : "No";
            }

            if (value is bool? && (value as bool?).HasValue)
            {
                return (bool)value ? "Yes" : "No";
            }

            return value.ToString() ?? string.Empty;
        }
        public static object? FormatForDataverse(
            PropertyInfo prop,
            object value,
            DataverseLinkAttribute link
        )
        {
            if (link.TargetType == null)
                return null;

            var dtoType =
                Nullable.GetUnderlyingType(prop.PropertyType)
                ?? prop.PropertyType;

            var targetType =
                Nullable.GetUnderlyingType(link.TargetType)
                ?? link.TargetType;

            // Enum -> OptionSetValue
            if (dtoType.IsEnum &&
                targetType == typeof(OptionSetValue))
            {
                return new OptionSetValue(
                    Convert.ToInt32(value)
                );
            }

            if (value is Array array && array.GetType().GetElementType()?.IsEnum == true)
            {
                return new OptionSetValueCollection(
                    array.Cast<Enum>()
                        .Select(val => new OptionSetValue(Convert.ToInt32(val)))
                        .ToList()
                );
            }

            // Guid -> EntityReference
            if (targetType == typeof(EntityReference))
            {
                return CreateEntityReference(
                    prop,
                    value);
            }

            // Guid -> string
            if (dtoType == typeof(Guid) && targetType == typeof(string))
            {
                return value.ToString();
            }

            // string -> Guid
            if (dtoType == typeof(string) && targetType == typeof(Guid))
            {
                if (!Guid.TryParse(value.ToString(), out Guid result))
                {
                    throw new InvalidPluginExecutionException($"Guid parse error from {value}");
                }

                return result;
            }

            if (targetType.IsAssignableFrom(dtoType))
            {
                return value;
            }

            return Convert.ChangeType(
                value,
                targetType);
        }
        public static EntityReference? CreateEntityReference(
            PropertyInfo property,
            object value
        )
        {
            if (value is not Guid id) return null;

            var reference = property.GetCustomAttribute<DataverseReferenceAttribute>();

            if (reference == null) return null;

            return new EntityReference(
                reference.TargetEntity,
                id
            );
        }
    }
}