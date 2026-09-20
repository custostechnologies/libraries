using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        string CreateHtmlObject(TDto dto);
        Guid MapToNotificationRequest(TDto dto, string htmlContent);
    }
}