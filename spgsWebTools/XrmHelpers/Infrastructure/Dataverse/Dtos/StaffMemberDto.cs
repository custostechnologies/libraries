using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.NewUserSyncEvent.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SynStaffMember
    {
        // identity

        /// <summary>
        /// 
        /// </summary>
        public string? SchoolStaffCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid SynMirrorId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? SourceOfTruthId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? StaffId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? CommunityGuid { get; set; }

        // status

        /// <summary>
        /// 
        /// </summary>
        public bool ActiveFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OptionSetValue? Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OptionSetValue? StatusReason { get; set; }

        // personal details
        /// <summary>
        /// 
        /// </summary>
        public string? Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Given1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Given2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? NameInternal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? MaidenName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? StaffPreferredName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? SurnamePrevious { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Surname { get; set; }


        // contact details
        /// <summary>
        /// 
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? OccupEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? OccupPhone { get; set; }

        // work details
        /// <summary>
        /// 
        /// </summary>
        public string? StaffCampus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? StaffCategory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? StaffDepartment { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? NetworkLogin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? SubSchool { get; set; }

        // converted option set values
        /// <summary>
        /// 
        /// </summary>
        public string? CampusKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? GenderKey { get; set; }
        /// <summary>
        /// 
        /// </summary>

        // dates
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LeavingDate { get; set; }

        // change detection
        /// <summary>
        /// 
        /// </summary>
        public string? PlaintextHash { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long EncodedHash { get; set; }
    }
}