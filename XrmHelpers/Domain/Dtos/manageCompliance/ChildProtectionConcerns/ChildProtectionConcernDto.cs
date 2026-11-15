using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

using custos.XrmHelpers.Domain.Attributes;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Domain.Contracts;
using custos.XrmHelpers.Infrastructure.Dataverse;

namespace custos.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class ChildProtectionConcernDto : NotifiableDto<ChildProtectionConcernDto>, IValidationMetadata, IQueryDto
    {
        public ChildProtectionConcernDto() : base() { }
        public override string TargetAppDisplayName => "manageCompliance";

        public override string SourceEntityLogicalName => EntityLogicalNames.ChildProtectionConcern;
        [DataverseLink(
            srcName: SynStaffMemberFields.ForcePlugin,
            srcType: typeof(DateTime?),
            trgName: SynStaffMemberFields.ForcePlugin,
            trgType: typeof(DateTime?))]
        public override DateTime? ForcePlugin { get; set; } = null;

        [IgnoreOnCreate]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.ChildProtectionConcernId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null)]
        public Guid ChildProtectionConcernId { get; set; }
        public override Guid EntityId { get => ChildProtectionConcernId; set => ChildProtectionConcernId = value; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.AttachmentId,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.AttachmentId,
            trgType: typeof(string))]
        public string? AttachmentId { get; set; }

        [Notification(operationType: OperationType.Create, label: "Title", index: 0, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Title", index: 0, true)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Title", index: 0, true)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.AutoGenTitle,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.AutoGenTitle,
            trgType: typeof(string))]
        public string? Name { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.CommunityId,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.CommunityId,
            trgType: typeof(string))]
        public string? CommunityId { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernDetail,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.ConcernDetail,
            trgType: typeof(string))]
        public string? ConcernDetail { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernId,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.ConcernId,
            trgType: typeof(string))]
        public string? ConcernId { get; set; }

        [EnumType(typeof(ConcernType))]
        [Notification(operationType: OperationType.UpdateOwner, label: "Concern Type(s)", index: 3, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Concern Type(s)", index: 3, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernTypes,
            srcType: typeof(OptionSetValueCollection),
            trgName: ChildProtectionConcernFields.ConcernTypes,
            trgType: typeof(OptionSetValueCollection))]
        public ConcernType[]? ConcernTypes { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.Contact,
            srcType: typeof(EntityReference),
            trgName: ChildProtectionConcernFields.Contact,
            trgType: typeof(EntityReference))]
        public Guid? ContactId { get; set; }

        [EnumType(typeof(ConcernType))]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernType,
            srcType: typeof(OptionSetValue),
            trgName: ChildProtectionConcernFields.ConcernType,
            trgType: typeof(OptionSetValue))]
        public ConcernType? ConcernType { get; set; }

        [EnumType(typeof(ConcernFlag))]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.Flag,
            srcType: typeof(OptionSetValue),
            trgName: ChildProtectionConcernFields.Flag,
            trgType: typeof(OptionSetValue))]
        public ConcernFlag? Flag { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.HaveActionsBeenAssigned,
            srcType: typeof(bool?),
            trgName: ChildProtectionConcernFields.HaveActionsBeenAssigned,
            trgType: typeof(bool?))]
        public bool? HaveActionsBeenAssigned { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.HistoryOfAbuseNeglectOrHarm,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.HistoryOfAbuseNeglectOrHarm,
            trgType: typeof(string))]
        public string? HistoryOfAbuseNeglectOrHarm { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.InvestigationAndFurtherInformation,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.InvestigationAndFurtherInformation,
            trgType: typeof(string))]
        public string? InvestigationAndFurtherInformation { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.JumpOffset,
            srcType: typeof(int?),
            trgName: ChildProtectionConcernFields.JumpOffset,
            trgType: typeof(int?))]
        public int? JumpOffset { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ParentInformation,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.ParentInformation,
            trgType: typeof(string))]
        public string? ParentInformation { get; set; }

        [EnumType(typeof(RecordSource))]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.RecordSource,
            srcType: typeof(OptionSetValue),
            trgName: ChildProtectionConcernFields.RecordSource,
            trgType: typeof(OptionSetValue))]
        public RecordSource? RecordSource { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ReportingActionsRequired,
            srcType: typeof(OptionSetValue),
            trgName: ChildProtectionConcernFields.ReportingActionsRequired,
            trgType: typeof(OptionSetValue))]
        public int? ReportingActionsRequired { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentAddress,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.StudentAddress,
            trgType: typeof(string))]
        public string? StudentAddress { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentAge,
            srcType: typeof(int?),
            trgName: ChildProtectionConcernFields.StudentAge,
            trgType: typeof(int?))]
        public int? StudentAge { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentDisability,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.StudentDisability,
            trgType: typeof(string))]
        public string? StudentDisability { get; set; }

        [EnumType(typeof(StudentGender))]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentGender,
            srcType: typeof(OptionSetValue),
            trgName: ChildProtectionConcernFields.StudentGender,
            trgType: typeof(OptionSetValue))]
        public StudentGender? StudentGender { get; set; }

        [Notification(operationType: OperationType.Create, label: "Student Name", index: 1, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Student Name", index: 1, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Student Name", index: 1, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentName,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.StudentName,
            trgType: typeof(string))]
        public string? StudentName { get; set; }

        [EnumType(typeof(StudentType))]
        [Notification(operationType: OperationType.Create, label: "Student Type", index: 2, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Student Type", index: 2, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Student Type", index: 2, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentType,
            srcType: typeof(OptionSetValue),
            trgName: ChildProtectionConcernFields.StudentType,
            trgType: typeof(OptionSetValue))]
        public StudentType? StudentType { get; set; }

        [Notification(operationType: OperationType.Create, label: "Submitter Name", index: 3, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Submitter Name", index: 4, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Submitter Name", index: 4, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.SubmitterName,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.SubmitterName,
            trgType: typeof(string))]
        public string? SubmitterName { get; set; }

        [Notification(operationType: OperationType.Create, label: "Submitter Email", index: 4, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Submitter Email", index: 5, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.SubmitterEmail,
            srcType: typeof(string),
            trgName: ChildProtectionConcernFields.SubmitterEmail,
            trgType: typeof(string))]
        public string? SubmitterEmail { get; set; }

        [IgnoreOnCreate]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.CoreStudent,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public EntityReference? Student { get; set; }

        [IgnoreOnCreate]
        [DataverseReference(
            targetEntity: EntityLogicalNames.CoreStudent
        )]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.CoreStudent,
            srcType: typeof(EntityReference),
            trgName: ChildProtectionConcernFields.CoreStudent,
            trgType: typeof(EntityReference))]
        public Guid? CoreStudentId { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.CoreStudent,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public string? CoreStudentName { get; set; }
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