using System.Text;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DigestMetric
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class DigestDto<TDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<TDto> Records { get; set; } = [];
        public int TotalCount => Records.Count;
        public List<DigestMetric> Metrics { get; set; } = [];
        public string BuildMetricsTable(IStyleProvider styleProvider)
        {
            if (Metrics == null || Metrics.Count == 0) return string.Empty;

            var sb = new StringBuilder();

            sb.AppendLine(@$"<table style='{styleProvider.InlineTableStyle}' >");

            foreach (var metric in Metrics)
            {
                sb.AppendLine($@"
                    <tr style='{styleProvider.TableRowData}'>
                        <th style='{styleProvider.TableHeadColumn}'>{metric.Label}</th>
                        <td style='{styleProvider.TableRowData}'>{metric.Value}</td>
                    </tr>
                ");
            }

            sb.AppendLine("</table>");

            return sb.ToString();
        }
    }
}