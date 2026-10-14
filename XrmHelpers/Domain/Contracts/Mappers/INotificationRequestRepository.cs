using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationRequestRepository
    {
        Guid SaveRequest(
            NotificationRequestDto dto,
            string content
        );
    }
}