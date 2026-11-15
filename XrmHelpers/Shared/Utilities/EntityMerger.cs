using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Shared.Utilities
{
    public static class EntityMerger
    {
        // NOTE: This method will overwrite attributes in entity a with attributes from entity b if they have the same key.
        public static Entity Merge(Entity a, Entity b)
        {
            var merged = new Entity(a.LogicalName) { Id = a.Id };

            foreach (var att in a.Attributes)
            {
                merged[att.Key] = att.Value;
            }

            foreach (var att in b.Attributes)
            {
                merged[att.Key] = att.Value;
            }

            return merged;
        }
    }
}