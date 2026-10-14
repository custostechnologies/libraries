namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services
{
    public interface IStyleProvider
    {
        string InlineTableStyle { get; }
        string TableBodyRowEven { get; }
        string TableBodyRowOdd { get; }
        string TableHeadColumn { get; }
        string TableRowData { get; }
        string LinkStyle { get; }
    }
}