using System;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class GetAttributeDelta
    {
<<<<<<< HEAD
        public static T? Execute<T>(string attributeName, Entity target, Entity? preImage)
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
=======
        public static T? Execute<T>(string attributeName, Entity a, Entity? b)
        {
            try
            {
                if (a.Contains(attributeName)) return a.GetAttributeValue<T>(attributeName);
                if (b != null && b.Contains(attributeName)) return b.GetAttributeValue<T>(attributeName);
>>>>>>> source-temp/refactor/decouple-xrmhelpers
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}, attributeName: {1}", ex, attributeName);
                throw;
            }

            return default;
        }
    }
}