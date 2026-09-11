using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ILookupResolution<T>
    {
        public string TargetEntityLogicalName { get; set; }
        public T TargetRecordKey { get; set; }
        public class ResolvedLookups
        {
            public EntityReference[] Refs { get; set; }
        }
        public class UnresolvedLookups
        {

        }
    }
}