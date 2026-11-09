using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class SafetyIncidentDto : NotifiableDto<SafetyIncidentDto>, IValidationMetadata, IQueryDto
    {
        public SafetyIncidentDto() : base() { }
        public override string TargetAppDisplayName => "manageCompliance";
        public override string SourceEntityLogicalName => EntityLogicalNames.SafetyIncident;

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: null,
            trgType: null
        )]
        public override DateTime? ForcePlugin { get; set; } = null;

        [IgnoreOnCreate]
        [DataverseLink(
            srcName: SafetyIncidentFields.SafetyIncidentId,
            srcType: typeof(Guid),
            trgName: SafetyIncidentFields.SafetyIncidentId,
            trgType: typeof(Guid)
        )]
        public Guid SafetyIncidentId { get; set; }
        public override Guid EntityId { get => SafetyIncidentId; set => SafetyIncidentId = value; }

        [DataverseLink(
            srcName: SafetyIncidentFields.ActionToPreventFutureInjuries,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.ActionToPreventFutureInjuries,
            trgType: typeof(string)
        )]
        public string? ActionToPreventFutureInjuries { get; set; }

        [EnumType(typeof(AdmittedToHospital))]
        [DataverseLink(
            srcName: SafetyIncidentFields.AdmittedToHospital,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.AdmittedToHospital,
            trgType: typeof(OptionSetValue)
        )]
        public AdmittedToHospital? AdmittedToHospital { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.AmbulanceCalled,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.AmbulanceCalled,
            trgType: typeof(bool)
        )]
        public bool AmbulanceCalled { get; set; } = false;

        [DataverseLink(
            srcName: SafetyIncidentFields.AttachmentId,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.AttachmentId,
            trgType: typeof(string)
        )]
        public string? AttachmentId { get; set; }

        [Notification(operationType: OperationType.Create, label: "Title", index: 0, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Title", index: 0, true)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Title", index: 0, true)]
        [DataverseLink(
            srcName: SafetyIncidentFields.AutoGenTitle,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.AutoGenTitle,
            trgType: typeof(string)
        )]
        public string? AutoGenTitle { get; set; }

        [EnumType(typeof(IncidentCampus))]
        [Notification(operationType: OperationType.Create, label: "Campus", index: 4, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Campus", index: 4, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.Campus,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.Campus,
            trgType: typeof(OptionSetValue)
        )]
        public IncidentCampus? Campus { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.CommunityId,
            srcType: typeof(int),
            trgName: SafetyIncidentFields.CommunityId,
            trgType: typeof(int)
        )]
        public int CommunityId { get; set; } = 0;

        [DataverseLink(
            srcName: SafetyIncidentFields.Consequences,
            srcType: typeof(int?),
            trgName: SafetyIncidentFields.Consequences,
            trgType: typeof(int?)
        )]
        public int? Consequences { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.ConsequencesMoreDetails,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.ConsequencesMoreDetails,
            trgType: typeof(string)
        )]
        public string? ConsequencesMoreDetails { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DaysInHospitalTxt,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DaysInHospitalTxt,
            trgType: typeof(string)
        )]
        public string? DaysInHospitalTxt { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DaysOpen,
            srcType: typeof(decimal?),
            trgName: SafetyIncidentFields.DaysOpen,
            trgType: typeof(decimal?)
        )]
        public decimal? DaysOpen { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DaysOpenInt,
            srcType: typeof(int?),
            trgName: SafetyIncidentFields.DaysOpenInt,
            trgType: typeof(int?)
        )]
        public int? DaysOpenInt { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DetailsOfGovernmentAgencyNotification,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DetailsOfGovernmentAgencyNotification,
            trgType: typeof(string)
        )]
        public string? DetailsOfGovernmentAgencyNotification { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DetailsOfInjury,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DetailsOfInjury,
            trgType: typeof(string)
        )]
        public string? DetailsOfInjury { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DetailsOfInsurerNotification,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DetailsOfInsurerNotification,
            trgType: typeof(string)
        )]
        public string? DetailsOfInsurerNotification { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DetailsOfOtherMedicalTreatment,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DetailsOfOtherMedicalTreatment,
            trgType: typeof(string)
        )]
        public string? DetailsOfOtherMedicalTreatment { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasAnyOtherPersonInjuredIfYesInputName,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.WasAnyOtherPersonInjuredIfYesInputName,
            trgType: typeof(string)
        )]
        public string? WasAnyOtherPersonInjuredIfYesInputName { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DetailsOfParentNotification,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DetailsOfParentNotification,
            trgType: typeof(string)
        )]
        public string? DetailsOfParentNotification { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DetailsOfRTWProgram,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.DetailsOfRTWProgram,
            trgType: typeof(string)
        )]
        public string? DetailsOfRTWProgram { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.DidInjuryResultInALossOfTime,
            srcType: typeof(bool),
            trgName: SafetyIncidentFields.DidInjuryResultInALossOfTime,
            trgType: typeof(bool)
        )]
        public bool DidInjuryResultInALossOfTime { get; set; } = false;

        [DataverseLink(
            srcName: SafetyIncidentFields.DoesHazardRegistersNeedUpdating,
            srcType: typeof(bool),
            trgName: SafetyIncidentFields.DoesHazardRegistersNeedUpdating,
            trgType: typeof(bool)
        )]
        public bool DoesHazardRegistersNeedUpdating { get; set; } = false;

        [DataverseLink(
            srcName: SafetyIncidentFields.EstimatedTimeOffWorkOrSchool,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.EstimatedTimeOffWorkOrSchool,
            trgType: typeof(string)
        )]
        public string? EstimatedTimeOffWorkOrSchool { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.FirstAidDetailsWhoWhat,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.FirstAidDetailsWhoWhat,
            trgType: typeof(string)
        )]
        public string? FirstAidDetailsWhoWhat { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.FurtherCorrectiveActionNeeded,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.FurtherCorrectiveActionNeeded,
            trgType: typeof(string)
        )]
        public string? FurtherCorrectiveActionNeeded { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.ImmediateCorrectiveActionTaken,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.ImmediateCorrectiveActionTaken,
            trgType: typeof(string)
        )]
        public string? ImmediateCorrectiveActionTaken { get; set; }

        [EnumType(typeof(IncidentCategory))]
        [Notification(operationType: OperationType.Create, label: "Incident Category", index: 3, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Incident Category", index: 3, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.IncidentCategory,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.IncidentCategory,
            trgType: typeof(OptionSetValue)
        )]
        public IncidentCategory IncidentCategory { get; set; }

        [Notification(operationType: OperationType.Create, label: "Incident Date/Time", index: 1, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Incident Date/Time", index: 1, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.DateTimeOfIncident,
            srcType: typeof(DateTime),
            trgName: SafetyIncidentFields.DateTimeOfIncident,
            trgType: typeof(DateTime)
        )]
        public DateTime DateTimeOfIncident { get; set; } = DateTime.Now;

        [EnumType(typeof(IncidentTimeframe))]
        [DataverseLink(
            srcName: SafetyIncidentFields.IncidentTimeframe,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.IncidentTimeframe,
            trgType: typeof(OptionSetValue)
        )]
        public IncidentTimeframe? IncidentTimeframe { get; set; }

        [EnumType(typeof(IncidentType))]
        [Notification(operationType: OperationType.Create, label: "Incident Type", index: 2, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Incident Type", index: 2, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Type", index: 4, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.IncidentType,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.IncidentType,
            trgType: typeof(OptionSetValue)
        )]
        public IncidentType IncidentType { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.InjuredPersonEmailAddress,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.InjuredPersonEmailAddress,
            trgType: typeof(string)
        )]
        public string? InjuredPersonEmailAddress { get; set; }

        [EnumType(typeof(InjuryBodyPart))]
        [DataverseLink(
            srcName: SafetyIncidentFields.InjuryBodyPart,
            srcType: typeof(OptionSetValueCollection),
            trgName: SafetyIncidentFields.InjuryBodyPart,
            trgType: typeof(OptionSetValueCollection)
        )]
        public InjuryBodyPart[]? InjuryBodyPart { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.InvestigationOfInjuryIncident,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.InvestigationOfInjuryIncident,
            trgType: typeof(string)
        )]
        public string? InvestigationOfInjuryIncident { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.IsARTWProgramRequired,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.IsARTWProgramRequired,
            trgType: typeof(bool?)
        )]
        public bool? IsARTWProgramRequired { get; set; }

        [EnumType(typeof(Location))]
        [Notification(operationType: OperationType.Create, label: "Location", index: 5, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Location", index: 5, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.Location,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.Location,
            trgType: typeof(OptionSetValue)
        )]
        public Location? Location { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.MedicationAdministrationWhatWhen,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.MedicationAdministrationWhatWhen,
            trgType: typeof(string)
        )]
        public string? MedicationAdministrationWhatWhen { get; set; }

        [Notification(operationType: OperationType.Create, label: "Name of Person Involved", index: 6, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Name of Person Involved", index: 6, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Person Involved", index: 2, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.NameOfInjuredPerson,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.NameOfInjuredPerson,
            trgType: typeof(string)
        )]
        public string? NameOfInjuredPerson { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.NameOfSupervisorOfDuty,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.NameOfSupervisorOfDuty,
            trgType: typeof(string)
        )]
        public string? NameOfSupervisorOfDuty { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.NeedsWorkersCompClaimYN,
            srcType: typeof(bool),
            trgName: SafetyIncidentFields.NeedsWorkersCompClaimYN,
            trgType: typeof(bool)
        )]
        public bool NeedsWorkersCompClaimYN { get; set; } = false;

        [DataverseLink(
            srcName: SafetyIncidentFields.ParentGuardianNotified,
            srcType: typeof(bool),
            trgName: SafetyIncidentFields.ParentGuardianNotified,
            trgType: typeof(bool)
        )]
        public bool ParentGuardianNotified { get; set; } = false;

        [DataverseLink(
            srcName: SafetyIncidentFields.PCFSingleLineTextTestField,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.PCFSingleLineTextTestField,
            trgType: typeof(string)
        )]
        public string? PCFSingleLineTextTestField { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.PropertyDamageDetails,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.PropertyDamageDetails,
            trgType: typeof(string)
        )]
        public string? PropertyDamageDetails { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.RecordClosedDate,
            srcType: typeof(DateTime?),
            trgName: SafetyIncidentFields.RecordClosedDate,
            trgType: typeof(DateTime?)
        )]
        public DateTime? RecordClosedDate { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.RecordClosedOn,
            srcType: typeof(DateTime?),
            trgName: SafetyIncidentFields.RecordClosedOn,
            trgType: typeof(DateTime?)
        )]
        public DateTime? RecordClosedOn { get; set; }

        [EnumType(typeof(RecordSource))]
        [Notification(operationType: OperationType.Create, label: "Source", index: 11, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Source", index: 11, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.RecordSource,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.RecordSource,
            trgType: typeof(OptionSetValue)
        )]
        public RecordSource RecordSource { get; set; } = RecordSource.MyCompliance;

        [EnumType(typeof(RelevantSport))]
        [DataverseLink(
            srcName: SafetyIncidentFields.RelevantSport,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.RelevantSport,
            trgType: typeof(OptionSetValue)
        )]
        public RelevantSport? RelevantSport { get; set; }

        [EnumType(typeof(ResponsePriority))]
        [Notification(operationType: OperationType.UpdateOwner, label: "Response Priority", index: 10, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.ResponsePriority,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.ResponsePriority,
            trgType: typeof(OptionSetValue)
        )]
        public ResponsePriority IncidentResponsePriority
        {
            get
            {
                switch (SeverityClassification)
                {
                    case IncidentSeverity.Negligible:
                    case IncidentSeverity.Low:
                        return ResponsePriority.Routine;
                    case IncidentSeverity.Medium:
                        return ResponsePriority.Priority;
                    case IncidentSeverity.High:
                        return ResponsePriority.Urgent;
                    case IncidentSeverity.Critical:
                        return ResponsePriority.Immediate;
                    default:
                        return ResponsePriority.Routine;
                }
            }
        }

        [DataverseLink(
            srcName: SafetyIncidentFields.SafetyManagerNotified,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.SafetyManagerNotified,
            trgType: typeof(bool?)
        )]
        public bool? SafetyManagerNotified { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.SchoolNurseDetails,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.SchoolNurseDetails,
            trgType: typeof(string)
        )]
        public string? SchoolNurseDetails { get; set; }
        
        [EnumType(typeof(SeverityLevel))]
        [Notification(operationType: OperationType.UpdateOwner, label: "Severity Classification", index: 9, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.SeverityClassification,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.SeverityClassification,
            trgType: typeof(OptionSetValue)
        )]
        public IncidentSeverity SeverityClassification
        {
            get
            {
                var output = IncidentExtensions.SeverityScore(
                    IncidentType,
                    AmbulanceCalled,
                    AdmittedToHospital ?? Enums.AdmittedToHospital.No,
                    DidInjuryResultInALossOfTime,
                    IncidentCategory,
                    InjuryBodyPart,
                    TypeOfInjury
                );

                var score = output.severityScore;
                var message = output.logMessage;

                if (score < 3) return IncidentSeverity.Negligible;
                else if (score >= 3 && score < 8 ) return IncidentSeverity.Low;
                else if (score >= 8 && score < 12 ) return IncidentSeverity.Medium;
                else if (score >= 12 && score < 15 ) return IncidentSeverity.High;
                else return IncidentSeverity.Critical;
            }
        }

        [DataverseLink(
            srcName: SafetyIncidentFields.StatusV2,
            srcType: typeof(int?),
            trgName: SafetyIncidentFields.StatusV2,
            trgType: typeof(int?)
        )]
        public int? StatusV2 { get; set; }

        [Notification(operationType: OperationType.Create, label: "Submitter Email", index: 8, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Submitter Email", index: 8, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.SubmitterEmail,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.SubmitterEmail,
            trgType: typeof(string)
        )]
        public string? SubmitterEmail { get; set; }

        [Notification(operationType: OperationType.Create, label: "Submitter Name", index: 7, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Submitter Name", index: 7, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Submitted By", index: 3, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.SubmitterName,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.SubmitterName,
            trgType: typeof(string)
        )]
        public string? SubmitterName { get; set; }

        [EnumType(typeof(SubmitterPosition))]
        [DataverseLink(
            srcName: SafetyIncidentFields.SubmitterPosition,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.SubmitterPosition,
            trgType: typeof(OptionSetValue)
        )]
        public SubmitterPosition? SubmitterPosition { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.SubmittingCoreUserId,
            srcType: typeof(Guid?),
            trgName: SafetyIncidentFields.SubmittingCoreUserId,
            trgType: typeof(Guid?)
        )]
        public Guid? SubmittingCoreUserId { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.SuggestedChangesToHazardRegister,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.SuggestedChangesToHazardRegister,
            trgType: typeof(string)
        )]
        public string? SuggestedChangesToHazardRegister { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.SummaryOfInjuryOutcome,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.SummaryOfInjuryOutcome,
            trgType: typeof(string)
        )]
        public string? SummaryOfInjuryOutcome { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.Tags,
            srcType: typeof(int?),
            trgName: SafetyIncidentFields.Tags,
            trgType: typeof(int?)
        )]
        public int? Tags { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.TagsV2,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.TagsV2,
            trgType: typeof(string)
        )]
        public string? TagsV2 { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.TemporaryRecordId,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.TemporaryRecordId,
            trgType: typeof(string)
        )]
        public string? TemporaryRecordId { get; set; }

        [EnumType(typeof(Term))]
        [DataverseLink(
            srcName: SafetyIncidentFields.Term,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.Term,
            trgType: typeof(OptionSetValue)
        )]
        public Term? Term { get; set; }

        [EnumType(typeof(InjuryType))]
        [DataverseLink(
            srcName: SafetyIncidentFields.TypeOfInjury,
            srcType: typeof(OptionSetValueCollection),
            trgName: SafetyIncidentFields.TypeOfInjury,
            trgType: typeof(OptionSetValueCollection)
        )]
        public InjuryType[]? TypeOfInjury { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasAGovtAgencyNotified,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasAGovtAgencyNotified,
            trgType: typeof(bool?)
        )]
        public bool? WasAGovtAgencyNotified { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasASupervisorPresent,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasASupervisorPresent,
            trgType: typeof(bool?)
        )]
        public bool? WasASupervisorPresent { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasAWorkersCompClaimLodged,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasAWorkersCompClaimLodged,
            trgType: typeof(bool?)
        )]
        public bool? WasAWorkersCompClaimLodged { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasAnyOtherPersonInjured,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasAnyOtherPersonInjured,
            trgType: typeof(bool?)
        )]
        public bool? WasAnyOtherPersonInjured { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasFirstAidProvided,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.WasFirstAidProvided,
            trgType: typeof(string)
        )]
        public string? WasFirstAidProvided { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasFirstAidProvidedYN,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasFirstAidProvidedYN,
            trgType: typeof(bool?)
        )]
        public bool? WasFirstAidProvidedYN { get; set; }

        [Notification(operationType: OperationType.Create, label: "Sports Related?", index: 12, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Sports Related?", index: 12, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.WasItSportsRelated,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasItSportsRelated,
            trgType: typeof(bool?)
        )]
        public bool? WasItSportsRelated { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasMedicationAdministered,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasMedicationAdministered,
            trgType: typeof(bool?)
        )]
        public bool? WasMedicationAdministered { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasOurInsurerNotified,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasOurInsurerNotified,
            trgType: typeof(bool?)
        )]
        public bool? WasOurInsurerNotified { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasTheSchoolNurseNotified,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasTheSchoolNurseNotified,
            trgType: typeof(bool?)
        )]
        public bool? WasTheSchoolNurseNotified { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WasThereAnyPropertyDamage,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WasThereAnyPropertyDamage,
            trgType: typeof(bool?)
        )]
        public bool? WasThereAnyPropertyDamage { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WereThereAnyWitnesses,
            srcType: typeof(bool?),
            trgName: SafetyIncidentFields.WereThereAnyWitnesses,
            trgType: typeof(bool?)
        )]
        public bool? WereThereAnyWitnesses { get; set; }

        [EnumType(typeof(InjuredPersonType))]
        [Notification(operationType: OperationType.Create, label: "Who Was Injured?", index: 13, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Who Was Injured?", index: 13, false)]
        [DataverseLink(
            srcName: SafetyIncidentFields.WhoWasInjured,
            srcType: typeof(OptionSetValue),
            trgName: SafetyIncidentFields.WhoWasInjured,
            trgType: typeof(OptionSetValue)
        )]
        public InjuredPersonType WhoWasInjured { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.WitnessDetails,
            srcType: typeof(string),
            trgName: SafetyIncidentFields.WitnessDetails,
            trgType: typeof(string)
        )]
        public string? WitnessDetails { get; set; }

        [DataverseLink(
            srcName: SafetyIncidentFields.Year,
            srcType: typeof(int?),
            trgName: SafetyIncidentFields.Year,
            trgType: typeof(int?)
        )]
        public int? Year { get; set; }

        public override string LinkToRecord(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        )
        {
            return base.LinkToRecord(
                context,
                entityLogicalName,
                entityId,
                targetAppDisplayName);
        }

        public override string GenerateSummaryTable(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        )
        {
            return base.GenerateSummaryTable(
                context,
                entityLogicalName,
                entityId,
                targetAppDisplayName);
        }
    }
}