using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using Microsoft.Extensions.Options;

/*
Attribute
    = metadata

Mapper
    = field translation

Resolver
    = lookup handling

Validator
    = validation

ChangeDetector
    = change tracking
*/

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    public enum SyncStatus
    {
        Pending = 1,
        Validated = 2,
        Mapped = 3,
        Created = 4,
        Updated = 5,
        Failed = 6,
        Skipped = 7
    }
    public enum SyncOperationStatus
    {
        Pending = 1,
        Started = 2,
        Succeeded = 3,
        Failed = 4,
        Skipped = 5
    }
    interface IBusinessData<T>
    {
        public T DataObject { get; set; }
    }
    interface ISyncMetadata
    {
        public Guid? SourceId { get; set; }
        public string? SourceEntityLogicalName { get; set; } 
        public int SourceVersion { get; set; }
        public Guid? TargetId { get; set; }
        public string? TargetEntityLogicalName { get; set; }
        public int TargetVersion { get; set; }
        public bool IsFullSync { get; set; } 
        public int? CurrentHash { get; set; }
        public int? PreviousHash { get; set; }
        public bool? HasChanged { get; set; } 
        public DateTime? LastSyncedOn { get; set; }
        public Guid CorrelationId { get; set; }
        public SyncStatus Status { get; set; }
    }
    interface ISyncOperation
    {
        public string Operation { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public SyncOperationStatus Status { get; set; }
    }
    interface ILookupResolution
    {
        public class ResolvedLookups
        {
            public EntityReference[]        
        }
        public class UnresolvedLookups
        {
            
        }
    }
    interface IValidationMetadata
    {
        public class Errors
        {
            public string? Message { get; set; }
        }
        public class Warnings
        {
            public string? Meessage { get; set; }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class StudentSnapshotDTO
    {
        public static string SourceEntityLogicalName = EntityLogicalNames.SynStudent;
        public static string TargetEntityLogicalName = EntityLogicalNames.CoreStudent;
        // identity
        [DataverseLink(srcName: SynStudentFields.SynMirrorId,  srcType: typeof(Guid), trgName: null, trgType: null)]
        public Guid? SourceRecordId { get; set; }

        
        [DataverseLink(srcName: null,  srcType: null, trgName: CoreStudentFields.CoreStudentId, trgType: typeof(Guid))]
        public Guid? TargetRecordId { get; set; }
        

        [DataverseLink(srcName: null,  srcType: null, trgName: null, trgType: null)]
        public Guid? SourceOfTruthId { get; set; }

        
        [DataverseLink(srcName: SynStudentFields.StudentId,  srcType: typeof(int), trgName: CoreStudentFields.StudentId, trgType: typeof(int))]
        public int StudentId { get; set; }


        [DataverseLink(srcName: SynStudentFields.MutePlugin,  srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? MutePlugin { get; set; }

        
        [DataverseLink(srcName: SynStudentFields.StudentActiveFlag,  srcType: typeof(bool), trgName: CoreStudentFields.ActiveFlag, trgType: typeof(bool))]
        public bool? IsActive { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentCampus,  srcType: typeof(string), trgName: CoreStudentFields.Campus, trgType: typeof(Campus))]
        public Campus Campus { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentGender,  srcType: typeof(string), trgName: CoreStudentFields.Gender, trgType: typeof(Gender))]
        public Gender Gender { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentGiven1,  srcType: typeof(string), trgName: CoreStudentFields.Given1, trgType: typeof(string))]
        public string? Given1 { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentGiven2,  srcType: typeof(string), trgName: CoreStudentFields.Given2, trgType: typeof(string))]
        public string? Given2 { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentPreferred,  srcType: typeof(string), trgName: CoreStudentFields.Preferred, trgType: typeof(string))]
        public string? Preferred { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentSurname,  srcType: typeof(string), trgName: CoreStudentFields.Surname, trgType: typeof(string))]
        public string? Surname { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentHouse,  srcType: typeof(string), trgName: CoreStudentFields.House, trgType: typeof(House))]
        public House House { get; set; }


        [DataverseLink(srcName: SynStudentFields.StudentYearLevel,  srcType: typeof(int), trgName: CoreStudentFields.YearLevel, trgType: typeof(int))]
        public int? YearLevel { get; set; }


        [DataverseLink(srcName: SynStudentFields.Age,  srcType: typeof(int), trgName: CoreStudentFields.Age, trgType: typeof(int))]
        public int? Age { get; set; }


        [DataverseLink(srcName: SynStudentFields.Status,  srcType: typeof(OptionSetValue), trgName: CoreStudentFields.Status, trgType: typeof(OptionSetValue))]
        public OptionSetValue? Status { get; set; }

        
        [DataverseLink(srcName: SynStudentFields.StatusReason,  srcType: typeof(OptionSetValue), trgName: CoreStudentFields.StatusReason, trgType: typeof(Options))]
        public OptionSetValue? StatusReason { get; set; }


        [DataverseLink(srcName: SynStudentFields.EncodedHash,  srcType: typeof(int), trgName: null, trgType: null)]
        public int? EncodedHash { get; set; }


        [DataverseLink(srcName: SynStudentFields.PlaintextHash,  srcType: typeof(string), trgName: null, trgType: null)]
        public string? PlaintextHash { get; set; }


        [DataverseLink(srcName: SynStudentFields.LastSuccessfulSync,  srcType: typeof(DateTime), trgName: CoreStudentFields.LastSync, trgType: typeof(DateTime))]
        public DateTime? LastSuccessfulSync { get; set; }


        [DataverseLink(srcName: SynStudentFields.IgnoreSync,  srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? IgnoreSync { get; set; }

    }
}