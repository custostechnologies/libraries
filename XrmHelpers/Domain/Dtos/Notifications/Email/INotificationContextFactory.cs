using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface INotificationContextFactory
    {
        NotificationContext Create<TDto>(
            TDto dto,
            OperationType type,
            string recipientAddress,
            string recipientName,
            string cc
        ) where TDto : INotifiableDto;

        NotificationContext CreateSummary<TDto>(
            IReadOnlyCollection<TDto> dtos,
            OperationType type,
            string recipientAddress,
            string recipientName,
            string cc
        ) where TDto : INotifiableDto;
    }
}