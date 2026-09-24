using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.FunctionHelpers.Utilities
{
    public static class EntityRetrieveRequest
    {
        public static async Task<Entity> Build(
            ServiceClient client,
            ColumnSet colSet,
            EntityReference target
        )
        {
            RetrieveRequest request = new RetrieveRequest()
            {
                ColumnSet = colSet,
                Target = target
            };
            RetrieveResponse response = (RetrieveResponse)client.Execute(request);
            if (response.Entity == null)
            {
                var message = $"BuildEntityRetrieveRequest targeting Entity: {target.LogicalName} returned NULL. Exiting.";
                throw new InvalidOperationException(message);
            }
            Entity entity = response.Entity;
            Console.WriteLine("Returning entity with Id: {0}, Entity LogicalName: {1} to caller.", entity.Id, entity.LogicalName);
            return entity;
        }
    }
}