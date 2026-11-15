namespace custos.XrmHelpers.Infrastructure.Dataverse.Models
{
    public interface ITeam
    {
        string Name { get; init; }
        Guid TeamId { get; init; }
        List<User> Members { get; set; }
    }
    public class Team : ITeam
    {
        public string Name { get; init; } = string.Empty;
        public Guid TeamId { get; init; }
        public List<User> Members { get; set; } = [];
    }
}