using System;
using Microsoft.Xrm.Sdk;

namespace Custos.XrmHelpers.Infrastructure.Dataverse
{
    public class GetAttributeDelta
    {
        public static T? Execute<T>(string attributeName, Entity a, Entity? b)
        {
            try
            {
                if (a.Contains(attributeName)) return a.GetAttributeValue<T>(attributeName);
                if (b != null && b.Contains(attributeName)) return b.GetAttributeValue<T>(attributeName);
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