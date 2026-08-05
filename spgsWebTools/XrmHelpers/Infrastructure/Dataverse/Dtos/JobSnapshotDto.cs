using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class JobAssignmentSnapshot
    {
        // identity
        public Guid SynMirrorId { get; set; }
        public string? Name { get; set; }
        public int StaffJobPositionSeq { get; set; }

        // job position
        public EntityReference? CoreJobPosition { get; set; }
        public string? CoreJobPositionIdx { get; set; }
        public string? SynReportsTo { get; set; }

        // user
        public EntityReference? CoreStaffMember { get; set; }
        public string? CoreStaffMemberIdx { get; set; }
        public int? StaffId { get; set; }

        // status
        public int? IsActive { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }

        // metadata
        public decimal? AssignmentFte { get; set; }
        public decimal? FteRequired { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // sync
        public string? PlaintextHash { get; set; }
        public long? LongEncodedHash { get; set; }
    }
}