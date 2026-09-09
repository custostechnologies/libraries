using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.Infrastructure.Dataverse
{
    public class GetEnvironmentMetadata {
        public static Guid Id(IPluginExecutionContext context)
        {
            return context.OrganizationId;
        }
        public static string FriendlyName(
            IPluginExecutionContext context,
            IOrganizationService service
        )
        {
            Entity org = service.Retrieve(
                EntityLogicalNames.Organization,
                context.OrganizationId,
                new ColumnSet("name")
            );

            return org.GetAttributeValue<string>("name");
        } 
    }
}