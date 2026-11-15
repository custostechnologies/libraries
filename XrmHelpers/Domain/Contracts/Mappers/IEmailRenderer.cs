namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IEmailRenderer
    {
        string Render(
            NotificationTemplate template,
            IReadOnlyCollection<IInjectionTag> tags
        );
    }
}