using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Infrastructure.Dataverse.Dtos
{
        /// <summary>
        /// 
        /// </summary>
        public class StaffMemberSnapshot
        {
                // identity


                public string? SchoolStaffCode { get; set; }

                public Guid SynMirrorId { get; set; }

                public string? SourceOfTruthId { get; set; }

                public int StaffId { get; set; }

                public string? CommunityGuid { get; set; }

                // status


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

                public string? Surname { get; set; }


                // contact details

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

                public bool IgnoreSync { get; set; }
        }
}