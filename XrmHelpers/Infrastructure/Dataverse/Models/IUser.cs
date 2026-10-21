namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Models
{
    public interface IUser
    {
        string FullName { get; init; }
        string FirstName { get; init; }
        string LastName { get; init; }
        Guid Id { get; init; }
        string Email { get; init; }
    }
    public class User : IUser
    {
        public string FullName { get; init; } = string.Empty;
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public Guid Id { get; init; }
        public string Email { get; init; } = string.Empty;
    }
}