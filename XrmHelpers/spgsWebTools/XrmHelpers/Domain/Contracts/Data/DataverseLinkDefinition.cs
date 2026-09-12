namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class DataverseLinkDefinition
    {
        public PropertyInfo Property { get; init; }

        public string SourceAttribute { get; init; }

        public string TargetAttribute { get; init; }

        public Type SourceType { get; init; }

        public Type TargetType { get; init; }
    }
}