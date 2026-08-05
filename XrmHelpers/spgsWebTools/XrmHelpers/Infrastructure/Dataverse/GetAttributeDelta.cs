using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class GetAttributeDelta
    {
        public static T? Execute<T>(string attributeName, Entity target, Entity preImage)
        {
            try
            {
                if (target.Contains(attributeName))
                {
                    return target.GetAttributeValue<T>(attributeName);
                }

                if (preImage != null && preImage.Contains(attributeName))
                {
                    return preImage.GetAttributeValue<T>(attributeName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}, attributeName: {1}", ex, attributeName);
                throw;
            }

            return default;
        }

        /* overloaded helper to retrieve value from one entity */
        public static T? Execute<T>(string attributeName, Entity entity)
        {
            try
            {
                if (entity != null && entity.Contains(attributeName))
                {
                    return entity.GetAttributeValue<T>(attributeName);
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in GetValue<T>(attributeName, Entity): {0}", ex);
                throw;
            }
        }
    }
}