namespace Custos.XrmHelpers.Domain.Contracts
{
    public class NotificationContext
    {
        public INotifiableDto? Dto { get; init; }
        public string To { get; set; } = string.Empty;
        public string CC { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public string Table { get; set; } = string.Empty; 
        public string ButtonText { get; set; } = string.Empty;
        public string ButtonUrl { get; set; } = string.Empty;
    }
}