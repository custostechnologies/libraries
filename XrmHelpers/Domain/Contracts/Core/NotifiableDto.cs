using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Enums;
using System.Text;
using System.Reflection;
using System.Net;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Organization;
using Custos.XrmHelpers.Infrastructure.Dataverse;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class NotifiableDto<TDto> : INotifiableDto
    {
        protected NotifiableDto() : base() { }
        public virtual Guid EntityId { get; set; }
        public virtual string TargetAppDisplayName { get; set; } = string.Empty;
        public virtual string ValidationRuleSet { get; set; } = string.Empty;
        public virtual string SourceEntityLogicalName { get; set; } = string.Empty;
        public virtual string TargetEntityLogicalName { get; set; } = string.Empty;
        public virtual string HelpMeLink { get; set; } = "https://my.stpauls.nsw.edu.au/homepage/24256";
        public virtual OperationType ContextOperationType { get; set; }
        public virtual DateTime? ForcePlugin { get; set; } = null;
        public virtual List<DigestMetric> Metrics { get; } = [];

        public virtual KeyAttributeCollection? KeyCollection =>
            new KeyAttributeCollection { };

        public class HydratedTag
        {
            public HydratedTag(string val, string label, int idx)
            {
                Value = val;
                Label = label;
                Index = idx;
            }
            public string Value { get; set; } = string.Empty;
            public string Label { get; set; } = string.Empty;
            public int Index { get; set; }
        }
        public virtual string LinkToRecord(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        )
        {
            string envUrl = string.Empty;
            Guid appId = Guid.Empty;

            var _context = context;
            var _logger = _context.GetLogger<NotifiableDto<TDto>>();

            try
            {
                var req = new RetrieveCurrentOrganizationRequest();
                var response = (RetrieveCurrentOrganizationResponse)_context.Runtime.UserService.Execute(req);

                if (response.Detail.Endpoints.ContainsKey(EndpointType.WebApplication))
                {
                    envUrl = response.Detail.Endpoints[EndpointType.WebApplication];
                }

                QueryExpression query = new QueryExpression("appmodule")
                {
                    ColumnSet = new ColumnSet("appmoduleid"),
                    Criteria = new FilterExpression()
                };

                // Filter by the unique name of the model-driven app
                query.Criteria.AddCondition("name", ConditionOperator.Equal, targetAppDisplayName);

                EntityCollection results = _context.Runtime.UserService.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    appId = results.Entities[0].Id;
                }
                else
                {
                    _logger.WriteLog("Unable to resolve app id from appmodule table");
                }

                var appIdString = appId != Guid.Empty ? $"appid={appId}&" : "";

                return $"{envUrl}main.aspx?{appIdString}pagetype=entityrecord&etn={entityLogicalName}&id={entityId}";
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException($"Failed to find the environment URL: ", ex);
            }
        }
        public virtual string GenerateSummaryTable(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        )
        {
            var _context = context;
            StringBuilder strBuilder = new StringBuilder();
            List<HydratedTag> hydratedTags = new List<HydratedTag>();

            Type type = this.GetType();
            OperationType operationType = ContextOperationType;

            foreach (var prop in type.GetProperties())
            {
                var notificationAttributes = prop.GetCustomAttributes<NotificationAttribute>();
                var link = prop.GetCustomAttribute<DataverseLinkAttribute>();
                string str = string.Empty;

                if (!notificationAttributes.Any() || link == null) continue;

                var attribute = notificationAttributes.FirstOrDefault(attr => attr.OperationType == operationType);

                if (attribute == null) continue;

                var value = prop.GetValue(this);
                var idx = attribute.Idx;
                var label = attribute.Label;

                var recordLink = LinkToRecord(
                    _context,
                    entityLogicalName,
                    entityId,
                    targetAppDisplayName
                );

                if (attribute.Label == "Title")
                {
                    str = $"<a href=\"{recordLink}\" target=\"_blank\">{prop.GetValue(this)}</a>";
                }
                else if (value is Array array && array.GetType().GetElementType()?.IsEnum == true)
                {
                    str = string.Join(", ", array.Cast<Enum>());
                    Console.WriteLine($"In Array Branch, str: {str}");
                }
                else
                {
                    str = WebUtility.HtmlEncode(value?.ToString() ?? "[NULL]");
                }

                //rawValues[attribute.Label ?? "[NULL]"] = str;
                hydratedTags.Add(new HydratedTag(
                    str, label, idx
                ));
            }

            hydratedTags.Sort((a, b) => a.Index.CompareTo(b.Index));

            strBuilder.AppendLine("<table class=\"table table - dark table - striped\" role=\"presentation\"border = \"0\" cellpadding = \"0\" cellspacing = \"0\"style = \"border-radius: 0px; border-collapse: separate !important; width: 100%; overflow: hidden; border: 1px solid #e2e8f0;\"bgcolor = \"#ffffff\" > ");

            strBuilder.AppendLine("<thead>");
            strBuilder.AppendLine("<tr style=\"line - height: 18px; font - size: 12px; margin: 0; padding: 6px; border: 1px solid #e2e8f0;\" align=\"left\" valign=\"top\" scope=\"row\">");
            strBuilder.AppendLine("<th>Key</th>");
            strBuilder.AppendLine("<th>Value</th>");
            strBuilder.AppendLine("</tr>");
            strBuilder.AppendLine("</thead>");

            strBuilder.AppendLine("<tbody>");

            int iterator = 0;

            foreach (var tag in hydratedTags)
            {
                var bgcolor = iterator % 2 == 0 ? "#F2F2F2" : "";
                strBuilder.AppendLine($"<tr style=\"line-height: 18px; font-size: 12px; margin: 0; padding: 6px; border: 1px solid #e2e8f0;\" align=\"left\" valign=\"top\" scope=\"row\" bgcolor=\"{bgcolor}\">");
                strBuilder.AppendLine($"<th style=\"line-height: 18px; font-size: 12px; margin: 0; padding: 6px; border-color: #e2e8f0; border-style: solid; border-width: 1px 1px 2px;\" align=\"center\" valign=\"top\" scope=\"col\">{tag.Label}</th>");
                strBuilder.AppendLine($"<td>{tag.Value}</td>");
                strBuilder.AppendLine("</tr>");

                iterator++;
            }

            iterator = 0;

            strBuilder.AppendLine("</tbody>");
            strBuilder.AppendLine("</table>");

            return strBuilder.ToString();
        }
    }
}