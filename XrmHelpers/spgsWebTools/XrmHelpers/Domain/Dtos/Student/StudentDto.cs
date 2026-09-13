using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

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

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentDto : IValidationMetadata, IDto
    {
        const string _srcEntityName = EntityLogicalNames.SynStudent;
        const string _targetEntityName = EntityLogicalNames.CoreStudent;
        public string ValidationRuleSet => "StrictStudentRules";
        public string SourceEntityLogicalName => EntityLogicalNames.SynStudent;
        public string TargetEntityLogicalName => EntityLogicalNames.CoreStudent;

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentId, srcType: typeof(int), trgName: CoreStudentFields.StudentId, trgType: typeof(int))]
        public int StudentId { get; set; }

        /* TEST 
        [DataverseLookup(keyProperty: nameof(StudentId), targetEntity: _srcEntityName, targetAttribute: "studentreference")]
        [DataverseLink(srcName: SynStudentFields.StudentId, srcType: typeof(int), trgName: _targetEntityName, trgType: typeof(EntityReference))]
        public EntityReference? SynReference { get; set; }*/

        // identity
        [Hash, Attributes.Key(keyType: "Primary"), AlwaysSync]
        [DataverseLink(srcName: SynStudentFields.SynMirrorId, srcType: typeof(Guid), trgName: null, trgType: null)]
        public Guid? SourceRecordId { get; set; }

        // satisfy IBusinessData
        public string EntityId => StudentId.ToString();

        [Hash, Attributes.Key(keyType: "Primary")]
        [DataverseLink(srcName: null, srcType: null, trgName: CoreStudentFields.CoreStudentId, trgType: typeof(Guid))]
        public Guid? TargetRecordId { get; set; }


        [DataverseLink(srcName: null, srcType: null, trgName: null, trgType: null)]
        public Guid? SourceOfTruthId { get; set; }


        [DataverseLink(srcName: SynStudentFields.MutePlugin, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? MutePlugin { get; set; }


        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentActiveFlag, srcType: typeof(bool), trgName: CoreStudentFields.ActiveFlag, trgType: typeof(bool))]
        public bool? IsActive { get; set; }


        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentCampus, srcType: typeof(string), trgName: CoreStudentFields.Campus, trgType: typeof(Campus))]
        public Campus Campus { get; set; }


        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentGender, srcType: typeof(string), trgName: CoreStudentFields.Gender, trgType: typeof(Gender))]
        public Gender Gender { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentGiven1, srcType: typeof(string), trgName: CoreStudentFields.Given1, trgType: typeof(string))]
        public string? Given1 { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentGiven2, srcType: typeof(string), trgName: CoreStudentFields.Given2, trgType: typeof(string))]
        public string? Given2 { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentPreferred, srcType: typeof(string), trgName: CoreStudentFields.Preferred, trgType: typeof(string))]
        public string? Preferred { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentSurname, srcType: typeof(string), trgName: CoreStudentFields.Surname, trgType: typeof(string))]
        public string? Surname { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentHouse, srcType: typeof(string), trgName: CoreStudentFields.House, trgType: typeof(House))]
        public House House { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.StudentYearLevel, srcType: typeof(int), trgName: CoreStudentFields.YearLevel, trgType: typeof(int))]
        public int? YearLevel { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.Age, srcType: typeof(int), trgName: CoreStudentFields.Age, trgType: typeof(int))]
        public int? Age { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStudentFields.Status, srcType: typeof(OptionSetValue), trgName: CoreStudentFields.Status, trgType: typeof(OptionSetValue))]
        public OptionSetValue? Status { get; set; }

        [DataverseLink(srcName: SynStudentFields.StatusReason, srcType: typeof(OptionSetValue), trgName: CoreStudentFields.StatusReason, trgType: typeof(Options))]
        public OptionSetValue? StatusReason { get; set; }

        [AlwaysSync, Hash(identifier: "EncodedHash")]
        [DataverseLink(srcName: SynStudentFields.EncodedHash, srcType: typeof(int), trgName: null, trgType: null)]
        public int? EncodedHash { get; set; }

        [AlwaysSync, Hash(identifier: "PlaintextHash")]
        [DataverseLink(srcName: SynStudentFields.PlaintextHash, srcType: typeof(string), trgName: null, trgType: null)]
        public string? PlaintextHash { get; set; }

        [AlwaysSync, TouchDateTime]
        [DataverseLink(srcName: SynStudentFields.LastSuccessfulSync, srcType: typeof(DateTime), trgName: CoreStudentFields.LastSync, trgType: typeof(DateTime))]
        public DateTime? LastSuccessfulSync { get; set; }


        [DataverseLink(srcName: SynStudentFields.IgnoreSync, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? IgnoreSync { get; set; }

    }
}