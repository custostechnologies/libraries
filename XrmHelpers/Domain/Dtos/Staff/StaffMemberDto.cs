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
using System.ComponentModel.Design;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class StaffMemberDto : IValidationMetadata, IDto
    {
        const string _srcEntityName = EntityLogicalNames.SynStaffMember;
        const string _targetEntityName = EntityLogicalNames.CoreStaffMember;
        public string ValidationRuleSet => "StrictStaffMemberRules";
        public string SourceEntityLogicalName => EntityLogicalNames.SynStaffMember;
        public string TargetEntityLogicalName => EntityLogicalNames.CoreStaffMember;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? ForcePlugin { get; set; } = null;

        [Hash, Attributes.Key(keyType: "Alternate")]
        [DataverseLink(srcName: SynStaffMemberFields.StaffId, srcType: typeof(int), trgName: CoreStaffMemberFields.StaffId, trgType: typeof(int))]
        public int StaffId { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.SchoolStaffCode, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SchoolStaffCode { get; set; }

        [Hash, Attributes.Key(keyType: "Primary")]
        [DataverseLink(srcName: SynStaffMemberFields.SynMirrorId, srcType: typeof(Guid), trgName: null, trgType: null)]
        public Guid? SourceRecordId { get; set; }

        [Hash, Attributes.Key(keyType: "Primary")]
        [DataverseLink(srcName: null, srcType: null, trgName: CoreStaffMemberFields.CoreStaffMemberId, trgType: typeof(Guid))]
        public Guid? TargetRecordId { get; set; }

        [Hash, Attributes.Key(keyType: "Alternate")]
        [DataverseLink(srcName: SynStaffMemberFields.SourceOfTruthId, srcType: typeof(string), trgName: CoreStaffMemberFields.SourceOfTruthId, trgType: typeof(string))]
        public Guid? SourceOfTruthId { get; set; }

        [Hash, Attributes.Key(keyType: "Alternate")]
        [DataverseLink(srcName: SynStaffMemberFields.CommunityGuid, srcType: typeof(string), trgName: null, trgType: null)]
        public Guid? CommunityGuid { get; set; }

        // status
        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.ActiveFlag, srcType: typeof(bool), trgName: CoreStaffMemberFields.IsActive, trgType: typeof(bool))]
        public bool ActiveFlag { get; set; }

        [DataverseLink(srcName: SynStaffMemberFields.Status, srcType: typeof(OptionSetValue), trgName: CoreStaffMemberFields.Status, trgType: typeof(OptionSetValue))]
        public OptionSetValue? Status { get; set; }

        [DataverseLink(srcName: SynStaffMemberFields.StatusReason, srcType: typeof(OptionSetValue), trgName: CoreStaffMemberFields.StatusReason, trgType: typeof(OptionSetValue))]
        public OptionSetValue? StatusReason { get; set; }

        // personal details
        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.Gender, srcType: typeof(string), trgName: CoreStaffMemberFields.Gender, trgType: typeof(Gender))]
        public Gender Gender { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.Given1, srcType: typeof(string), trgName: CoreStaffMemberFields.GivenName1, trgType: typeof(string))]
        public string? GivenName1 { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.Given2, srcType: typeof(string), trgName: CoreStaffMemberFields.GivenName2, trgType: typeof(string))]
        public string? GivenName2 { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.NameInternal, srcType: typeof(string), trgName: null, trgType: null)]
        public string? NameInternal { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.MaidenName, srcType: typeof(string), trgName: CoreStaffMemberFields.MaidenName, trgType: typeof(string))]
        public string? MaidenName { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.StaffPreferredName, srcType: typeof(string), trgName: CoreStaffMemberFields.PreferredName, trgType: typeof(string))]
        public string? PreferredName { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.SurnamePrevious, srcType: typeof(string), trgName: CoreStaffMemberFields.PreviousLastName, trgType: typeof(string))]
        public string? PreviousLastName { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.Surname, srcType: typeof(string), trgName: CoreStaffMemberFields.LastName, trgType: typeof(string))]
        public string? LastName { get; set; }

        // contact details
        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.Email, srcType: typeof(string), trgName: CoreStaffMemberFields.PersonalEmail, trgType: typeof(string))]
        public string? PersonalEmail { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.OccupEmail, srcType: typeof(string), trgName: CoreStaffMemberFields.WorkEmail, trgType: typeof(string))]
        public string? WorkEmail { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.OccupPhone, srcType: typeof(string), trgName: null, trgType: null)]
        public string? OccupPhone { get; set; }

        // work details
        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.StaffCampus, srcType: typeof(string), trgName: CoreStaffMemberFields.Campus, trgType: typeof(Campus))]
        public Campus Campus { get; set; }

        [DataverseLookup(keyProperty: nameof(StaffCategory), targetEntity: EntityLogicalNames.CoreStaffCategory, targetAttribute: "pgrowth_code")]
        [DataverseLink(srcName: SynStaffMemberFields.StaffCategory, srcType: typeof(string), trgName: CoreStaffMemberFields.CoreStaffCategory, trgType: typeof(EntityReference))]
        public EntityReference? CoreStaffCategory { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.StaffCategory, srcType: typeof(string), trgName: null, trgType: null)]
        public string? StaffCategory { get; set; }

        [DataverseLookup(keyProperty: nameof(StaffDepartment), targetEntity: EntityLogicalNames.CoreDepartment, targetAttribute: "pgrowth_code")]
        [DataverseLink(srcName: SynStaffMemberFields.StaffDepartment, srcType: typeof(string), trgName: CoreStaffMemberFields.CoreStaffCategory, trgType: typeof(EntityReference))]
        public EntityReference? CoreDepartment { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.StaffDepartment, srcType: typeof(string), trgName: null, trgType: null)]
        public string? StaffDepartment { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.NetworkLogin, srcType: typeof(string), trgName: CoreStaffMemberFields.NetworkLogin, trgType: typeof(string))]
        public string? NetworkLogin { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.SubSchool, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SubSchool { get; set; }

        // dates
        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.StartDate, srcType: typeof(DateTime?), trgName: CoreStaffMemberFields.StartDate, trgType: typeof(DateTime?))]
        public DateTime? StartDate { get; set; }

        [Hash]
        [DataverseLink(srcName: SynStaffMemberFields.LeavingDate, srcType: typeof(DateTime?), trgName: CoreStaffMemberFields.EndDate, trgType: typeof(DateTime?))]
        public DateTime? LeavingDate { get; set; }

        // change detection
        [AlwaysSync, Hash(identifier: "EncodedHash")]
        [DataverseLink(srcName: SynStaffMemberFields.EncodedHash, srcType: typeof(int), trgName: null, trgType: null)]
        public int? EncodedHash { get; set; }

        [AlwaysSync, Hash(identifier: "PlaintextHash")]
        [DataverseLink(srcName: SynStaffMemberFields.PlaintextHash, srcType: typeof(string), trgName: null, trgType: null)]
        public string? PlaintextHash { get; set; }

        [AlwaysSync, TouchDateTime]
        [DataverseLink(srcName: SynStaffMemberFields.LastSuccessfulSync, srcType: typeof(DateTime), trgName: CoreStaffMemberFields.LastSync, trgType: typeof(DateTime))]
        public DateTime? LastSuccessfulSync { get; set; }

        [DataverseLink(srcName: SynStaffMemberFields.IgnoreSync, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? IgnoreSync { get; set; }

        [DataverseLink(srcName: SynStaffMemberFields.MutePlugin, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? MutePlugin { get; set; }

        // CUST FIELD BUIDLING LOGIC
        [DataverseLink(srcName: null, srcType: null, trgName: CoreStaffMemberFields.DisplayName, trgType: typeof(string))]
        public string DisplayName => $"{PreferredName ?? GivenName1} {LastName}".Trim();

        [DataverseLink(srcName: null, srcType: null, trgName: CoreStaffMemberFields.FullName, trgType: typeof(string))]
        public string FullName => string.IsNullOrWhiteSpace(GivenName2)
            ? $"{GivenName1} {LastName}".Trim()
            : $"{GivenName1} {GivenName2} {LastName}".Trim();

        // KEY ATTRIBUTE COLLECTION
        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection
            {
                { CoreStaffMemberFields.StaffId, StaffId }
            };
    }
}