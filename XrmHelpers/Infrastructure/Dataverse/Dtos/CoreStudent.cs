using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    public class CoreStudent
    {
        // identity
        public Guid CoreStudentId { get; set; }
        public string? AzureObjectId { get; set; }
        public int StudentId { get; set; }
        public OptionSetValue? Campus { get; set; }
        public OptionSetValue? House { get; set; }
        public int? YearLevel { get; set; }

        // status
        public bool? ActiveFlag { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }

        // personal details
        public string? DisplayName { get; set; }
        public OptionSetValue? Gender { get; set; }
        public string? Preferred { get; set; }
        public string? Given1 { get; set; }
        public string? Given2 { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }

        // sync data
        public DateTime? LastSync { get; set; }

        // sync type
        public bool IsCreate { get; set; }

    }
}