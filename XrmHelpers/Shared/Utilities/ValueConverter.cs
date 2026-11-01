using System.Text.Json;

using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Shared.Utilities
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
                return Enum.ToObject(
                    targetType,
                    element.GetInt32());
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