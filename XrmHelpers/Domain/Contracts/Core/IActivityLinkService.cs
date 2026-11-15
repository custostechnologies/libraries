namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IActivityLinkService
    {
        string CreateLinkHtml(
            string entityLogicalName,
            string recordName,
            Guid entityId,
            string targetAppDisplayName
        );
    }
}