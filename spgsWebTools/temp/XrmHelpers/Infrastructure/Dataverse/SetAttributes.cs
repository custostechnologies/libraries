using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class SetAttributes
    {
        public static void Execute(string tableName, EntityReference targetRef, Dictionary<string, object> attributeDictionary, IOrganizationService service)
        {
            try
            {
                var targetEntity = new Entity(tableName, targetRef.Id);

                foreach (var field in attributeDictionary)
                {
                    targetEntity[field.Key] = field.Value;
                }

                service.Update(targetEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in UpdateTargetAttributes: {0}", ex);
                throw;
            }
        }
    }
}