using Microsoft.Xrm.Sdk;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace Custos.FunctionHelpers.Utilities
{
    public static class EntityQueries
    {
        public static async Task<Entity?> Build<T>(
            ServiceClient client,
            int topCount,
            ColumnSet colSet,
            string entityLogicalName,
            string targetAttributeName,
            T value
        )
        {
            QueryExpression query = new QueryExpression(entityLogicalName)
            {
                TopCount = topCount,
                ColumnSet = colSet,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(
                            targetAttributeName,
                            ConditionOperator.Equal,
                            value
                        )
                    }
                }
            };

            var response = await client.RetrieveMultipleAsync(query);
            if (response.Entities.Count == 0)
            {
                return null;
            }

            return response.Entities[0];
        }
    }
}