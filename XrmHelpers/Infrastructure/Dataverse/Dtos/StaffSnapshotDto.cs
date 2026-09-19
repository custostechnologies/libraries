using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class StaffMemberSnapshot
    {
        // identity

<<<<<<< HEAD
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
        public int StaffId { get; set; }
        /// <summary>
        /// 
        /// </summary>
=======

        public string? SchoolStaffCode { get; set; }

        public Guid SynMirrorId { get; set; }

        public string? SourceOfTruthId { get; set; }

        public int StaffId { get; set; }

>>>>>>> source-temp/refactor/decouple-xrmhelpers
        public string? CommunityGuid { get; set; }

        // status

<<<<<<< HEAD
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
=======

        public bool ActiveFlag { get; set; }

        public OptionSetValue? Status { get; set; }

        public OptionSetValue? StatusReason { get; set; }

        // personal details

        public string? Gender { get; set; }

        public string? Given1 { get; set; }

        public string? Given2 { get; set; }

        public string? NameInternal { get; set; }

        public string? MaidenName { get; set; }

        public string? StaffPreferredName { get; set; }

        public string? SurnamePrevious { get; set; }

>>>>>>> source-temp/refactor/decouple-xrmhelpers
        public string? Surname { get; set; }


        // contact details
<<<<<<< HEAD
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
        public int EncodedHash { get; set; }
        /// <summary>
        /// 
        /// </summary>
=======

        public string? Email { get; set; }

        public string? OccupEmail { get; set; }

        public string? OccupPhone { get; set; }

        // work details

        public string? StaffCampus { get; set; }

        public string? StaffCategory { get; set; }

        public string? StaffDepartment { get; set; }

        public string? NetworkLogin { get; set; }

        public string? SubSchool { get; set; }

        // converted option set values

        public string? CampusKey { get; set; }

        public string? GenderKey { get; set; }


        // dates
        public DateTime? StartDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        // change detection

        public string? PlaintextHash { get; set; }

        public int EncodedHash { get; set; }

>>>>>>> source-temp/refactor/decouple-xrmhelpers
        public bool IgnoreSync { get; set; }
    }
}