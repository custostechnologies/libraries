using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class JobPositionSnapshot
    {
        // identity
        public Guid SynMirrorId { get; set; }
        public string? Name { get; set; }
        public int JobPositionsSeq { get; set; }
        public int StaffJobPositionsSeq { get; set; }
        public string? SourceOfTruthId { get; set; }

        // job position
        public string? SynReportsTo { get; set; }
        public string? StaffJobPositionCode { get; set; }

        // user
        public int StaffId { get; set; }
        public string? StaffNameInternal { get; set; }

        // status
        public int IsActive { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }

        // metadata
        public decimal? AssignmentFte { get; set; }
        public decimal? FteRequired { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? JobPositionDescription { get; set; }

        // sync
        public string? PlaintextHash { get; set; }
        public int EncodedHash { get; set; }
    }
}