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
    public class ChildProtectionConcernDto : NotifiableDto<ChildProtectionConcernDto>, IValidationMetadata, IQueryDto
    {
        public ChildProtectionConcernDto() : base() { }
        public override string TargetAppDisplayName => "manageCompliance";

        public override string SourceEntityLogicalName => EntityLogicalNames.ChildProtectionConcern;

        [DataverseLink(
            srcName: SynStaffMemberFields.ForcePlugin, 
            srcType: typeof(DateTime?), 
            trgName: null, 
            trgType: null)]
        public override DateTime? ForcePlugin { get; set; } = null;

        [IgnoreOnCreate]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.ChildProtectionConcernId, 
            srcType: typeof(Guid), 
            trgName: null, 
            trgType: null)]
        public Guid ChildProtectionConcernId { get; set; }
        public override Guid EntityId { 
            get => ChildProtectionConcernId; 
            set => ChildProtectionConcernId = value; }
        [DataverseLink(
            srcName: ChildProtectionConcernFields.AttachmentId,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? AttachmentId { get; set; }
        
        [Notification(operationType: OperationType.Create, label: "Title", index: 0, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Title", index: 0, true)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Title", index: 0, true)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Name { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.CommunityId,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? CommunityId { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernDetail,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ConcernDetail { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernId,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ConcernId { get; set; }

        [Notification(operationType: OperationType.UpdateOwner, label: "Concern Type(s)", index: 3, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Concern Type(s)", index: 3, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernTypes,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public ConcernType[]? ConcernTypes { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.Contact,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public Guid? ContactId { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ConcernType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public ConcernType? ConcernType { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.Flag,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public ConcernFlag? Flag { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.HaveActionsBeenAssigned,
            srcType: typeof(bool?),
            trgName: null,
            trgType: null)]
        public bool? HaveActionsBeenAssigned { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.HistoryOfAbuseNeglectOrHarm,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? HistoryOfAbuseNeglectOrHarm { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.InvestigationAndFurtherInformation,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? InvestigationAndFurtherInformation { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.JumpOffset,
            srcType: typeof(int?),
            trgName: null,
            trgType: null)]
        public int? JumpOffset { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ParentInformation,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ParentInformation { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.RecordSource,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public RecordSource? RecordSource { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.ReportingActionsRequired,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? ReportingActionsRequired { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentAddress,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? StudentAddress { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentAge,
            srcType: typeof(int?),
            trgName: null,
            trgType: null)]
        public int? StudentAge { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentDisability,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? StudentDisability { get; set; }

        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentGender,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public StudentGender? StudentGender { get; set; }

        [Notification(operationType: OperationType.Create, label: "Student Name", index: 1, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Student Name", index: 1, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Student Name", index: 1, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentName,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? StudentName { get; set; }

        [Notification(operationType: OperationType.Create, label: "Student Type", index: 2, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Student Type", index: 2, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Student Type", index: 2, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.StudentType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public StudentType? StudentType { get; set; }

        [Notification(operationType: OperationType.Create, label: "Submitter Name", index: 3, false)]
        [Notification(operationType: OperationType.UpdateOwner, label: "Submitter Name", index: 4, false)]
        [Notification(operationType: OperationType.SummaryNotification, label: "Submitter Name", index: 4, false)]
        [DataverseLink(
            srcName: ChildProtectionConcernFields.SubmitterName,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public string? SubmitterName { get; set; }
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