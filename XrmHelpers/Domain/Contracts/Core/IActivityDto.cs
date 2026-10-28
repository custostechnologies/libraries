using System.Net;

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Organization;
using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IActivityDto
    {
        public abstract Guid RegardingRecordEntityId { get; }
        public abstract string RegardingRecordAppDisplayName { get; }
        public abstract string RegardingRecordEntityLogicalName { get; }
        public abstract string? RegardingRecordName { get; set; }
        public abstract string? RegardingRecordDisplayLink { get; set; }
    }
    public interface IActivityLinkService
    {
        string CreateLinkHtml(
            IDataverseContext context,
            string entityLogicalName,
            string recordName,
            Guid entityId,
            string targetAppDisplayName
        );
    }
    public class ActivityLinkService : IActivityLinkService
    {
        public string CreateLinkHtml(
            IDataverseContext context,
            string entityLogicalName,
            string recordName,
            Guid entityId,
            string targetAppDisplayName
        )
        {
            string envUrl = string.Empty;
            Guid appId = Guid.Empty;

            var _context = context;
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
                return $"<a href='{link}'>{encodedTitle}</a>";
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException($"Failed to find the environment URL: ", ex);
            }
        }
    }
}