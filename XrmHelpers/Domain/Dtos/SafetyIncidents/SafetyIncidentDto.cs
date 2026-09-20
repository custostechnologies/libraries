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
    public class SafetyIncidentDto : IValidationMetadata, IDto
    {
        const string _srcEntityName = EntityLogicalNames.SafetyIncident;
        const string _targetEntityName = EntityLogicalNames.SafetyIncident;
        public string ValidationRuleSet => "StrictSafetyIncidentRules";
        public string SourceEntityLogicalName => EntityLogicalNames.SafetyIncident;
        public string TargetEntityLogicalName => EntityLogicalNames.SafetyIncident;

        [DataverseLink(srcName: SynStaffMemberFields.ForcePlugin, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? ForcePlugin { get; set; } = null;

        public KeyAttributeCollection KeyCollection =>
            new KeyAttributeCollection { };

        [DataverseLink(srcName: SafetyIncidentFields.ActionToPreventFutureInjuries, srcType: typeof(string), trgName: null, trgType: null)]
        public string? ActionToPreventFutureInjuries { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.AdmittedToHospital, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public AdmittedToHospital AdmittedToHospital { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.AmbulanceCalled, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? AmbulanceCalled { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.AttachmentId, srcType: typeof(string), trgName: null, trgType: null)]
        public string? AttachmentId { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.AutoGenTitle, srcType: typeof(string), trgName: null, trgType: null)]
        public string? AutoGenTitle { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.Campus, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public Campus Campus { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.CommunityId, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? CommunityId { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.Consequences, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? Consequences { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.ConsequencesMoreDetails, srcType: typeof(string), trgName: null, trgType: null)]
        public string? ConsequencesMoreDetails { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DaysInHospitalTxt, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DaysInHospitalTxt { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DaysOpen, srcType: typeof(decimal?), trgName: null, trgType: null)]
        public decimal? DaysOpen { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DaysOpenInt, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? DaysOpenInt { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DetailsOfGovernmentAgencyNotification, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DetailsOfGovernmentAgencyNotification { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DetailsOfInjury, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DetailsOfInjury { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DetailsOfInsurerNotification, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DetailsOfInsurerNotification { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DetailsOfOtherMedicalTreatment, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DetailsOfOtherMedicalTreatment { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasAnyOtherPersonInjuredIfYesInputName, srcType: typeof(string), trgName: null, trgType: null)]
        public string? WasAnyOtherPersonInjuredIfYesInputName { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DetailsOfParentNotification, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DetailsOfParentNotification { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DetailsOfRTWProgram, srcType: typeof(string), trgName: null, trgType: null)]
        public string? DetailsOfRTWProgram { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DidInjuryResultInALossOfTime, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? DidInjuryResultInALossOfTime { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DoesHazardRegistersNeedUpdating, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? DoesHazardRegistersNeedUpdating { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.EstimatedTimeOffWorkOrSchool, srcType: typeof(string), trgName: null, trgType: null)]
        public string? EstimatedTimeOffWorkOrSchool { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.FirstAidDetailsWhoWhat, srcType: typeof(string), trgName: null, trgType: null)]
        public string? FirstAidDetailsWhoWhat { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.FurtherCorrectiveActionNeeded, srcType: typeof(string), trgName: null, trgType: null)]
        public string? FurtherCorrectiveActionNeeded { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.ImmediateCorrectiveActionTaken, srcType: typeof(string), trgName: null, trgType: null)]
        public string? ImmediateCorrectiveActionTaken { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.IncidentCategory, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public IncidentCategory IncidentCategory { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.DateTimeOfIncident, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? DateTimeOfIncident { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.IncidentTimeframe, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public IncidentTimeframe IncidentTimeframe { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.IncidentType, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public IncidentType IncidentType { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.InjuredPersonEmailAddress, srcType: typeof(string), trgName: null, trgType: null)]
        public string? InjuredPersonEmailAddress { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.InjuryBodyPart, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public InjuryBodyPart InjuryBodyPart { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.InvestigationOfInjuryIncident, srcType: typeof(string), trgName: null, trgType: null)]
        public string? InvestigationOfInjuryIncident { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.IsARTWProgramRequired, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? IsARTWProgramRequired { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.Location, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public Location Location { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.MedicationAdministrationWhatWhen, srcType: typeof(string), trgName: null, trgType: null)]
        public string? MedicationAdministrationWhatWhen { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.NameOfInjuredPerson, srcType: typeof(string), trgName: null, trgType: null)]
        public string? NameOfInjuredPerson { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.NameOfSupervisorOfDuty, srcType: typeof(string), trgName: null, trgType: null)]
        public string? NameOfSupervisorOfDuty { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.NeedsWorkersCompClaimYN, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? NeedsWorkersCompClaimYN { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.ParentGuardianNotified, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? ParentGuardianNotified { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.PCFSingleLineTextTestField, srcType: typeof(string), trgName: null, trgType: null)]
        public string? PCFSingleLineTextTestField { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.PropertyDamageDetails, srcType: typeof(string), trgName: null, trgType: null)]
        public string? PropertyDamageDetails { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.RecordClosedDate, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? RecordClosedDate { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.RecordClosedOn, srcType: typeof(DateTime?), trgName: null, trgType: null)]
        public DateTime? RecordClosedOn { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.RecordSource, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public RecordSource RecordSource { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.RelevantSport, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public RelevantSport RelevantSport { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.ResponsePriority, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? ResponsePriority { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.SafetyManagerNotified, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? SafetyManagerNotified { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.SchoolNurseDetails, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SchoolNurseDetails { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.SeverityClassification, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public SeverityLevel SeverityClassification { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.StatusV2, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? StatusV2 { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.SubmitterEmail, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SubmitterEmail { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.SubmitterName, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SubmitterName { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.SubmitterPosition, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? SubmitterPosition { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.SubmittingCoreUserId, srcType: typeof(Guid?), trgName: null, trgType: null)]
        public Guid? SubmittingCoreUserId { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.SuggestedChangesToHazardRegister, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SuggestedChangesToHazardRegister { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.SummaryOfInjuryOutcome, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SummaryOfInjuryOutcome { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.Tags, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? Tags { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.TagsV2, srcType: typeof(string), trgName: null, trgType: null)]
        public string? TagsV2 { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.TemporaryRecordId, srcType: typeof(string), trgName: null, trgType: null)]
        public string? TemporaryRecordId { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.Term, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public Term Term { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.TypeOfInjury, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public InjuryType TypeOfInjury { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasAGovtAgencyNotified, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasAGovtAgencyNotified { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasASupervisorPresent, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasASupervisorPresent { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasAWorkersCompClaimLodged, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasAWorkersCompClaimLodged { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasAnyOtherPersonInjured, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasAnyOtherPersonInjured { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasFirstAidProvided, srcType: typeof(string), trgName: null, trgType: null)]
        public string? WasFirstAidProvided { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasFirstAidProvidedYN, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasFirstAidProvidedYN { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasItSportsRelated, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasItSportsRelated { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasMedicationAdministered, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasMedicationAdministered { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasOurInsurerNotified, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasOurInsurerNotified { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasTheSchoolNurseNotified, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasTheSchoolNurseNotified { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WasThereAnyPropertyDamage, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WasThereAnyPropertyDamage { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WereThereAnyWitnesses, srcType: typeof(bool?), trgName: null, trgType: null)]
        public bool? WereThereAnyWitnesses { get; set; }

        [Notification]
        [DataverseLink(srcName: SafetyIncidentFields.WhoWasInjured, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public InjuredPersonType WhoWasInjured { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.WitnessDetails, srcType: typeof(string), trgName: null, trgType: null)]
        public string? WitnessDetails { get; set; }

        [DataverseLink(srcName: SafetyIncidentFields.Year, srcType: typeof(int?), trgName: null, trgType: null)]
        public int? Year { get; set; }
    }
}