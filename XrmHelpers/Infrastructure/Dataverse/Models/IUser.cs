namespace custos.XrmHelpers.Infrastructure.Dataverse.Models
{
    public interface IUser
    {
        string FullName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Guid Id { get; set; }
        string Email { get; set; }
    }
    public class User : IUser
    {
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}