using System.Reflection;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Mappers
{
    public static class EmploymentApplicationMapper
    {
        public static T Map<T>(Dictionary<string, string> values) where T : new()
        {
            var result = new T();
            foreach (var property in typeof(T).GetProperties())
            {
                var attribute = property.GetCustomAttribute<ApplicationFieldAttribute>();

                if (attribute == null) continue;
                if (!values.TryGetValue(attribute.FieldName, out var raw)) continue;
                if (string.IsNullOrWhiteSpace(raw)) continue;
                property.SetValue(result, ConvertValue(raw, property.PropertyType));
            }
            return result;
        }
        private static object? ConvertValue(string value, Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type == typeof(string)) return value;
            if (type == typeof(int)) return int.Parse(value);
            if (type == typeof(decimal)) return decimal.Parse(value);
            if (type == typeof(DateTime)) return DateTime.Parse(value);
            if (type == typeof(bool))
            {
                return value.Equals("yes", StringComparison.OrdinalIgnoreCase)
                    || value.Equals("true", StringComparison.OrdinalIgnoreCase)
                    || value.Equals("1", StringComparison.OrdinalIgnoreCase);
            }
            if (type.IsEnum) return Enum.Parse(type, value, true);

            return Convert.ChangeType(value, type);
        }
    }
}