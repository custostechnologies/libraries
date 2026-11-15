using System.Text;
using System.Net;
using System.Reflection;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Shared.Utilities;

namespace Custos.XrmHelpers.Infrastructure.Services
{
    public interface ISummaryField
    {
        string Label { get; set; }
        int Idx { get; set; }
        object? Value { get; set; }
        string? Url { get; set; }

    }
    public class SummaryField : ISummaryField
    {
        public string Label { get; set; } = string.Empty;
        public int Idx { get; set; }
        public object? Value { get; set; }
        public string? Url { get; set; } = string.Empty;
        public bool IsHtml { get; set; }

    }
    public static class SummaryNotificationService
    {
        private static readonly Dictionary<Type, PropertyInfo[]> _propCache = [];
        public static List<SummaryField> Build<TDto>(
            TDto dto, 
            OperationType type,
            Dictionary<Guid, string>? links
        ) where TDto : INotifiableDto
        {
            var dtoType = typeof(TDto);

            if (!_propCache.TryGetValue(dtoType, out var props))
            {
                props = dtoType.GetProperties();
                _propCache[dtoType] = props;
            }

            Console.WriteLine( "Build<{0}> cache contains key={1}",
                typeof(TDto).Name,
                _propCache.ContainsKey(typeof(TDto)));

            return _propCache[dtoType]
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
                                Value = prop.GetValue(dto),
                                Url = attr.IsLink
                                    && links != null
                                    && links.TryGetValue(dto.EntityId, out var url)
                                        ? url
                                        : null,
                                IsHtml = attr.IsHtml
                            }
                        )
                )
                .OrderBy(x => x.Idx)
                .ToList();
        }
        public static string BuildSingleEntityTable<TDto>(
            TDto dto, 
            OperationType type,
            IStyleProvider styleProvider
        ) where TDto : INotifiableDto
        {
            var fields = Build(dto, type, null);
            var sb = new StringBuilder();

            sb.AppendLine(@$"<table {styleProvider.InlineTableStyle}>");

            var idx = 0;
            foreach (var field in fields)
            {
                var label = WebUtility.HtmlEncode(field.Label);
                var formatted = ValueFormatter.Format(field.Value);
                var displayValue = field.IsHtml 
                    ? formatted
                    : WebUtility.HtmlEncode(formatted);
                bool even = idx % 2 == 0;

                var rowStyle = even ? styleProvider.TableBodyRowEven : styleProvider.TableBodyRowOdd;

                sb.AppendLine($@"
                    <tr>
                        <th {rowStyle} {styleProvider.TableHeadColumn}>{field.Label}</th>
                        <td {rowStyle} {styleProvider.TableRowData}>{displayValue}</td>
                    </tr>
                ");

                idx++;
            }

            sb.AppendLine("</table>");
            idx = 0;
            return sb.ToString();
        }
        public static string BuildMultiEntityTable<TDto>(
            IReadOnlyCollection<TDto> dtos, 
            Dictionary<Guid, string> links,
            OperationType type,
            IStyleProvider styleProvider
        ) where TDto : INotifiableDto
        {
            if (dtos == null || dtos.Count == 0) return string.Empty;

            var headerFields = Build(dtos.First(), type, links);
            var sb = new StringBuilder();

            sb.AppendLine($"<table {styleProvider.InlineTableStyle}>");
            // append table header row
            
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            foreach (var field in headerFields)
            {
                var label = WebUtility.HtmlEncode(field.Label);
                sb.AppendLine($"<th {styleProvider.TableHeadColumn}>{label}</th>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");

            sb.AppendLine("<tbody>");

            var idx = 0;
            foreach (var dto in dtos)
            {
                var values = Build(dto, type, links);
                bool even = idx % 2 == 0;
                var rowStyle = even ? styleProvider.TableBodyRowEven : styleProvider.TableBodyRowOdd;
                
                sb.AppendLine($@"<tr>");
                
                foreach (var val in values)
                {
                    var formatted = ValueFormatter.Format(val.Value);
                    var displayValue = val.IsHtml 
                        ? formatted
                        : WebUtility.HtmlEncode(formatted);
                    var encodedLink = WebUtility.HtmlEncode(val.Url);
                    if (!string.IsNullOrWhiteSpace(val.Url))
                    {
                        sb.AppendLine($@"<td {rowStyle} {styleProvider.TableRowData}>
                            <a href='{encodedLink}' target='_blank' style='{styleProvider.LinkStyle}'>{displayValue}</a>
                        </td>");
                    } else
                    {
                        sb.AppendLine($@"<td {rowStyle} {styleProvider.TableRowData}>{displayValue}</td>");
                    }
                }
                
                idx++;
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }
    }
}