using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface INotificationRequestRepository
    {
        Guid SaveRequest(
            NotificationRequestDto dto,
            string content
        );
    }
}