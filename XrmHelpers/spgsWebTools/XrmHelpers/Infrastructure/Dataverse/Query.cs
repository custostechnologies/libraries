namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public class Query<TKey> : IQuery<TKey>
    {
        public Query(
            string entityName,
            TKey key,
            string attributeName,
            ColumnSet cols,
            int? topCount
        )
        {
            TargetEntityLogicalName = entityName;
            TargetRecordKey = key;
            TargetAttributeLogicalName = attributeName;
            Columns = cols;
            TopCount = topCount;
        }
        public string TargetEntityLogicalName { get; }
        public TKey TargetRecordKey { get; }
        public string TargetAttributeLogicalName { get; }
        public ColumnSet Columns { get; }
        public int? TopCount { get; }
    }
}