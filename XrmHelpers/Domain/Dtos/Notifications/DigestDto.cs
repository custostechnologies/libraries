using System.Net;
using System.Text;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DigestDto<TDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Recipients { get; set; } = string.Empty;
        public IReadOnlyCollection<TDto> Records { get; set; } = [];
        public int TotalCount => Records.Count;
        public IReadOnlyCollection<DigestMetric> Metrics { get; set; } = [];
        public string BuildMetricsTable(
            IStyleProvider styleProvider
        )
        {
            if (Metrics == null || Metrics.Count == 0) return string.Empty;

            var sb = new StringBuilder();

            //sb.AppendLine(@$"<table {styleProvider.InlineTableStyle}>");

            var idx = 0;
            foreach (var metric in Metrics)
            {
                var label = WebUtility.HtmlEncode(metric.Label);
                var value = WebUtility.HtmlEncode(FormatValue(metric.Value));
                bool even = idx % 2 == 0;

                var rowStyle = even ? styleProvider.TableBodyRowEven : styleProvider.TableBodyRowOdd;

                /*sb.AppendLine($@"
                    <tr style='{rowStyle}'>
                        <th {styleProvider.TableHeadColumn}>{label}</th>
                        <td {styleProvider.TableRowData}>{value}</td>
                    </tr>
                ");*/
                sb.AppendLine(@$"
                    <p style='font-family: Arial, sans-serif; font-size: 16px; color: #333333;'>
                        {label}
                        <span style='display: inline-block; background-color: #17457a; color: #ffffff; font-size: 12px; font-weight: bold; padding: 2px 7px; border-radius: 10px; margin-left: 5px; vertical-align: middle;'>
                            {value}
                        </span>
                    </p>
                ");
            }

            //sb.AppendLine("</table>");

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