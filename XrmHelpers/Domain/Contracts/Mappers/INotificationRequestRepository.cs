using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface INotificationRequestRepository
    {
        Guid SaveRequest(
            NotificationRequestDto dto,
            string content
        );
    }
}