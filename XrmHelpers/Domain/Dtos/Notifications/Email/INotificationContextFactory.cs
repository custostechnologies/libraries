using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationContextFactory
    {
        NotificationContext Create<TDto>(
            TDto dto,
            OperationType type,
            string recipientAddress,
            string recipientName
        ) where TDto : INotifiableDto;

        NotificationContext CreateSummary<TDto>(
            List<TDto> dtos,
            OperationType type,
            string recipientAddress,
            string recipientName
        ) where TDto : INotifiableDto;
    }
}