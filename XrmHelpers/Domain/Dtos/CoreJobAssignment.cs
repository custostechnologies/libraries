using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public class CoreJobAssignment
    {
        public Guid CoreJobAssignmentId { get; set; }
        public string? Name { get; set; }
        public int? StaffJobPositionSeq { get; set; }
        public string? SourceOfTruthId { get; set; }
        public int? StaffId { get; set; }
        public EntityReference? CoreStaffMember { get; set; }
        public string? CoreStaffMemberIdx { get; set; }
        public EntityReference? CoreJobPosition { get; set; }
        public string? CoreJobPositionIdx { get; set; }
        public decimal? AssignmentFte { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? FteRequired { get; set; }
        public bool? IsActive { get; set; }
        public string? SynReportsTo { get; set; }
        // sync data
        public DateTime? LastSync { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }

        // sync type
        public bool IsCreate { get; set; }
    }
}