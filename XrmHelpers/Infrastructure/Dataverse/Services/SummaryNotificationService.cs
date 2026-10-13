using System.Text;
using System.Reflection;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Infrastructure.Services
{
    public interface ISummaryField
    {
        string Label { get; set; }
        int Idx { get; set; }
        object? Value { get; set; }

    }
    public class SummaryField
    {
        public string Label { get; set; } = string.Empty;
        public int Idx { get; set; }
        public object? Value { get; set; }

    }
    public static class SummaryNotificationService
    {
        public static List<SummaryField> Build<TDto>(TDto dto, OperationType type)
        {
            return typeof(TDto)
                .GetProperties()
                .SelectMany(
                    prop => prop
                        .GetCustomAttributes(
                            typeof(NotificationAttribute),
                            false
                        )
                        .Cast<NotificationAttribute>()
                        .Where(
                            attr => attr.OperationType == type
                        )
                        .Select(
                            attr => new SummaryField
                            {
                                Label = attr.Label,
                                Idx = attr.Idx,
                                Value = prop.GetValue(dto) switch
                                {
                                    null => string.Empty,
                                    DateTime dt => dt.ToString("dd/MM/yyyy"),
                                    DateTimeOffset dto => dto.ToString("dd/MM/yyyy HH:mm"),
                                    Enum e => e.ToString(),
                                    _ => prop.GetValue(dto)?.ToString()
                                }
                            }
                        )
                )
                .OrderBy(x => x.Idx)
                .ToList();
        }
        public static string BuildHtmlTable<TDto>(TDto dto, OperationType type)
        {
            var fields = Build(dto, type);

            var sb = new StringBuilder();

            sb.AppendLine(@"<table>");

            foreach (var field in fields)
            {
                var displayValue = FormatValue(field.Value);

                sb.AppendLine($@"
                    <tr>
                        <td>{field.Label}</td>
                        <td>{displayValue}</td>
                    </tr>
                ");
            }

            sb.AppendLine("</table>");

            return sb.ToString();
        }

        private static string FormatValue(object? value)
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
    }
}