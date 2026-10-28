using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Shared.Utilities
{
    public static class EntityMerger
    {
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