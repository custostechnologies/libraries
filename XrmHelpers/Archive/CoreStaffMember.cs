using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    public class CoreStaffMember
    {
        //[JsonPropertyName("pgrowth_corestaffmemberid")]
        // identity
        public Guid CoreStaffMemberId { get; set; }
        public string? AzureObjectId { get; set; }
        public string? SourceOfTruthId { get; set; }
        public int? StaffId { get; set; }
        public EntityReference? User { get; set; }

        // status
        public bool? IsActive { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }

        // job metadata
        public OptionSetValue? Campus { get; set; }
        public EntityReference? CoreDepartment { get; set; }
        public string? CoreDepartmentIdx { get; set; }
        public EntityReference? CoreStaffCategory { get; set; }
        public string? CoreStaffCategoryIdx { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? NetworkLogin { get; set; }
        public string? WorkEmail { get; set; }

        // personal details
        public string? DisplayName { get; set; }
        public string? FullName { get; set; }
        public OptionSetValue? Gender { get; set; }
        public string? PreferredName { get; set; }
        public string? GivenName1 { get; set; }
        public string? GivenName2 { get; set; }
        public string? LastName { get; set; }
        public string? PreviousLastName { get; set; }
        public string? MaidenName { get; set; }
        public string? PersonalEmail { get; set; }

        // group membership
        public bool? InUserGroup { get; set; }

        // flags
        public bool? ProfileResolutionFlag { get; set; }
        public bool? SPGSEmailAddressFlag { get; set; }
        public bool? StaffEmailAddressFlag { get; set; }

        // sync data
        public DateTime? LastSync { get; set; }

        // sync type
        public bool IsCreate { get; set; }

    }
}