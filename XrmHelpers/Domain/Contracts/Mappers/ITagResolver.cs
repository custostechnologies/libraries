namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ITagResolver
    {
        List<IInjectionTag> BuildTags(
            NotificationContext context,
            NotificationTemplate template
        );
    }
}