using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentSnapshot
    {
        // identity
        [DataverseLink(
            srcName: SynStudentFields.SynMirrorId, 
            srcType: typeof(Guid), 
            trgName: null, 
            trgType: null
        )]
        public Guid? SourceRecordId { get; set; }
        
        public Guid? CoreRecordId { get; set; }
        
        public int? StudentId { get; set; }
        public bool? MutePlugin { get; set; }
        public bool? StudentActiveFlag { get; set; }
        public string? StudentCampus { get; set; }
        public string? StudentGender { get; set; }
        public string? StudentGiven1 { get; set; }
        public string? StudentGiven2 { get; set; }
        public string? StudentPreferred { get; set; }
        public string? StudentSurname { get; set; }
        public string? StudentHouse { get; set; }
        public int? StudentYearLevel { get; set; }
        public int? Age { get; set; }

        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }

        public string? StudentCampusKey { get; set; }
        public string? StudentHouseKey { get; set; }
        public string? StudentGenderKey { get; set; }
        public int? EncodedHash { get; set; }
        public string? PlaintextHash { get; set; }
        public DateTime? LastSuccessfulSync { get; set; }
        public bool? IgnoreSync { get; set; }
    }
}