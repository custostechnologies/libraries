using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationMapper<TDto>
    {
        string CreateHtmlObject(TDto dto);
        Entity MapToNotificationRequest(TDto dto);
    }
}