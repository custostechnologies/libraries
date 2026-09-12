namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse
{
    public interface IQuery<TKey>
    {
        string TargetEntityLogicalName { get; }
        TKey TargetRecordKey { get; }
        string TargetAttributeLogicalName { get; }
        ColumnSet Columns { get; }
        int? TopCount { get; }
    }
}