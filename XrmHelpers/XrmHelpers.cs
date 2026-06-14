using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace spgsWebTools.XrmHelpers;

public class XrmHelpers
{
    public static T? GetValue<T>(string attributeName, Entity target, Entity entity)
    {
        if (target.Contains(attributeName))
        {
            return target.GetAttributeValue<T>(attributeName);
        }

        if (entity != null && entity.Contains(attributeName))
        {
            return entity.GetAttributeValue<T>(attributeName);
        }

        return default;
    }

    /* overloaded helper to retrieve value from one entity */
    public static T? GetValue<T>(string attributeName, Entity entity)
    {
        if (entity != null && entity.Contains(attributeName))
        {
            return entity.GetAttributeValue<T>(attributeName);
        } else
        {
            return default;
        }
    }

    // TO ADD: string array
    public static Microsoft.Xrm.Sdk.EntityCollection GetMultipleRecords<EntityCollection>(
        string entityName, 
        string targetAttributeValue, 
        string targetAttributeName, 
        string[] columnSet, 
        int returnCount, 
        int pageNum,
        string orderAttributeName,
        bool ascending,
        IOrganizationService service)
    {
        // define table query
        QueryByAttribute query = new(entityName)
        {
            ColumnSet = new ColumnSet(columnSet),
            PageInfo = new PagingInfo() { Count = returnCount, PageNumber = pageNum }
        };

        // add query parameters
        query.AddAttributeValue(targetAttributeName, targetAttributeValue);

        if (orderAttributeName != null && orderAttributeName != "")
        {
            query.AddOrder(orderAttributeName, ascending ? OrderType.Ascending : OrderType.Descending);   
        }

        // send query request
        Microsoft.Xrm.Sdk.EntityCollection queryResults = service.RetrieveMultiple(query);

        return queryResults;
    }

    public static void UpdateTargetAttributes<T>(string tableName, EntityReference targetRef, Dictionary<string, object> attributeDictionary, IOrganizationService service)
    {
        var targetEntity = new Entity(tableName, targetRef.Id);

        foreach (var field in attributeDictionary)
        {
            targetEntity[field.Key] = field.Value;
        }

        service.Update(targetEntity);
    }

    /* TO BE MOVED TO A SEPARATE UTILITIES PACKAGE */

    /* helper to calculate new hash using a standard BKDR hashing algorithm */
    /* mirrors hashing implementation in Power Query                        */
    /* takes a concatenated string (with '|' separator), converts each      */
    /* char to its corresponding integer, adds padding and then calculates  */
    /* the hash                                                             */
    public static string ComputeBKDRHashKeys(string plaintext)
    {

        long seed = 1313;
        long modulo = 2147483647;
        long state = 0;

        for (int i = 0; i < plaintext.Length; i++) 
        {
            // convert character to 3-digit integer
            int charInt = (int) plaintext[i];

            // convert integer into a string with padding
            string paddedStr = charInt.ToString("D3");

            // parse to long
            long current = long.Parse(paddedStr);

            // caculate state
            state = (state + (current * seed)) % modulo;                
        }
        
        return state.ToString();
    }
}
