using System.Net;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Organization;
using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Infrastructure.Dataverse;

namespace custos.XrmHelpers.Domain.Contracts
{
    public class ActivityLinkService : IActivityLinkService
    {
        private readonly IDataverseContext _context;
        public ActivityLinkService(IDataverseContext context)
        { _context = context; }
        public string CreateLinkHtml(
            string entityLogicalName,
            string recordName,
            Guid entityId,
            string targetAppDisplayName
        )
        {
            string envUrl = string.Empty;
            Guid appId = Guid.Empty;
            var _logger = _context.GetLogger<ActivityLinkService>();

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
                var link = $"{envUrl}main.aspx?{appIdString}pagetype=entityrecord&etn={entityLogicalName}&id={entityId}";
                var encodedTitle = WebUtility.HtmlEncode(recordName);
                return $"<a href='{link}' style='color: blue; text-decoration: underline; font-weight: bold'>{encodedTitle}</a>";
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException($"Failed to find the environment URL: ", ex);
            }
        }
    }
}