using System.Reflection;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DeltaComparison<TDto>
    {
        public bool HasChanged { get; init; }
        public IEnumerable<PropertyInfo> ChangedProperties { get; init; }
        public TDto OldRecord { get; init; }
        public TDto NewRecord { get; init; }
        public string OldPlaintextHash { get; init; }
        public string NewPlaintextHash { get; init; }
        public int OldEncodedHash { get; init; }
        public int NewEncodedHash { get; init; }
    }
}