using System.Text.Json;

using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Shared.Utilities
{
    public static class ValueConverter
    {
        public static object? FromJsonValue(
            JsonElement element,
            Type targetType)
        {
            if (element.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            if (targetType.IsEnum)
            {
                if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty("value", out var valueProperty))
                {
                    return Enum.ToObject(
                        targetType,
                        valueProperty.GetInt32()
                    );
                }

                return Enum.ToObject(
                    targetType,
                    element.GetInt32());
            }

            if (targetType.IsArray && element.ValueKind == JsonValueKind.Array)
            {
                var elementType = targetType.GetElementType();
                if (elementType == null) return null;
                var values = element
                    .EnumerateArray()
                    .Select(
                        item =>
                        {
                            if (item.ValueKind == JsonValueKind.Object && item.TryGetProperty("value", out var valProp))
                            {
                                return FromJsonValue(valProp, elementType);
                            }

                            return FromJsonValue(item, elementType);
                        }
                    ).ToArray();

                var array = Array.CreateInstance(
                    elementType,
                    values.Length
                );

                for (var i = 0; i < values.Length; i++)
                {
                    array.SetValue(values[i], i);
                }

                return array;
            }

            if (targetType == typeof(string))
            {
                return element.GetString();
            }

            if (targetType == typeof(bool))
            {
                return element.GetBoolean();
            }

            if (targetType == typeof(int))
            {
                return element.GetInt32();
            }

            if (targetType == typeof(long))
            {
                return element.GetInt64();
            }

            if (targetType == typeof(decimal))
            {
                return element.GetDecimal();
            }

            if (targetType == typeof(double))
            {
                return element.GetDouble();
            }

            if (targetType == typeof(Guid))
            {
                return Guid.Parse(element.GetString()!);
            }

            if (targetType == typeof(DateTime))
            {
                return element.GetDateTime();
            }

            if (targetType == typeof(DateTimeOffset))
            {
                return element.GetDateTimeOffset();
            }

            if (targetType == typeof(List<int>))
            {
                return element
                    .EnumerateArray()
                    .Select(x => x.GetInt32())
                    .ToList();
            }

            if (targetType.IsGenericType &&
                targetType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var itemType = targetType.GetGenericArguments()[0];

                var list = (System.Collections.IList)
                    Activator.CreateInstance(targetType)!;

                foreach (var item in element.EnumerateArray())
                {
                    list.Add(
                        FromJsonValue(item, itemType)
                    );
                }

                return list;
            }

            return JsonSerializer.Deserialize(
                element.GetRawText(),
                targetType);
        }
        public static OptionSetValue ToOptionSetValue(int enumValue)
        {
            return new OptionSetValue(enumValue);
        }
    }
}