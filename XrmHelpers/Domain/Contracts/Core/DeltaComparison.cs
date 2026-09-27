using System.Reflection;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DeltaComparison<TDto>
    {
        public bool HasChanged { get; init; }
        public required IEnumerable<PropertyInfo> ChangedProperties { get; init; }
        public required TDto OldRecord { get; init; }
        public required TDto NewRecord { get; init; }
        public required string OldPlaintextHash { get; init; }
        public required string NewPlaintextHash { get; init; }
        public int OldEncodedHash { get; init; }
        public int NewEncodedHash { get; init; }
    }
}