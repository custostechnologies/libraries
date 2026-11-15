using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Infrastructure.Dataverse;

namespace Custos.XrmHelpers.Domain.Dtos
{
    /// <summary>
    ///
    /// </summary>
    public class WorkersCompensationClaimDto : NotifiableDto<WorkersCompensationClaimDto>, IActivityDto, IValidationMetadata, IQueryDto
    {
        public WorkersCompensationClaimDto() : base() { }

        public override string TargetAppDisplayName => "spgsWebTools";
        public string RegardingRecordAppDisplayName => "manageCompliance";
        public string RegardingRecordEntityLogicalName => EntityLogicalNames.SafetyIncident;
        public override string SourceEntityLogicalName => EntityLogicalNames.WorkersCompensationClaim;
        public override string TargetEntityLogicalName => EntityLogicalNames.WorkersCompensationClaim;

        [IgnoreOnCreate]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ActivityId,
            srcType: typeof(Guid),
            trgName: WorkersCompensationClaimFields.ActivityId,
            trgType: typeof(Guid))]
        public Guid ActivityId { get; set; }

        public override Guid EntityId
        {
            get => ActivityId;
            set => ActivityId = value;
        }

        [DataverseLink(
            srcName: SynStaffMemberFields.ForcePlugin,
            srcType: typeof(DateTime?),
            trgName: SynStaffMemberFields.ForcePlugin,
            trgType: typeof(DateTime?))]
        public override DateTime? ForcePlugin { get; set; }

        [Notification(OperationType.Create, "Claim Number", 1, false)]
        [Notification(OperationType.UpdateOwner, "Claim Number", 1, false)]
        [Notification(OperationType.SummaryNotification, "Claim Number", 1, false)]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ClaimNumber,
            srcType: typeof(int?),
            trgName: WorkersCompensationClaimFields.ClaimNumber,
            trgType: typeof(int?))]
        public int? ClaimNumber { get; set; }

        [Notification(OperationType.Create, "Staff Member", 2, false)]
        [Notification(OperationType.UpdateOwner, "Staff Member", 2, false)]
        [Notification(OperationType.SummaryNotification, "Staff Member", 2, false)]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StaffMemberName,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.StaffMemberName,
            trgType: typeof(string))]
        public string? StaffMemberName { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StaffMemberEmail,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.StaffMemberEmail,
            trgType: typeof(string))]
        public string? StaffMemberEmail { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ClaimDescription,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.ClaimDescription,
            trgType: typeof(string))]
        public string? ClaimDescription { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.EmailMessageToEmployee,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.EmailMessageToEmployee,
            trgType: typeof(string))]
        public string? EmailMessageToEmployee { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.OtherKnownEmployment,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.OtherKnownEmployment,
            trgType: typeof(string))]
        public string? OtherKnownEmployment { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.PaymentsInformationReceived,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.PaymentsInformationReceived,
            trgType: typeof(string))]
        public string? PaymentsInformationReceived { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.LostTimeClaim,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.LostTimeClaim,
            trgType: typeof(string))]
        public string? LostTimeClaim { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ExpensesApproved,
            srcType: typeof(bool?),
            trgName: WorkersCompensationClaimFields.ExpensesApproved,
            trgType: typeof(bool?))]
        public bool? ExpensesApproved { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StaffType,
            srcType: typeof(OptionSetValue),
            trgName: WorkersCompensationClaimFields.StaffType,
            trgType: typeof(OptionSetValue))]
        public PersonType StaffType { get; set; }

        [IgnoreSrcMapping]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.RelatedUser,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public string? RelatedUserName { get; set; }

        [DataverseReference(targetEntity: "systemuser")]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.RelatedUser,
            srcType: typeof(EntityReference),
            trgName: WorkersCompensationClaimFields.RelatedUser,
            trgType: typeof(EntityReference))]
        public Guid RelatedUserId { get; set; }

        [Notification(OperationType.Create, "Title", 0, true)]
        [Notification(OperationType.UpdateOwner, "Title", 0, true)]
        [Notification(OperationType.SummaryNotification, "Title", 0, true)]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Subject,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.Subject,
            trgType: typeof(string))]
        public string? Subject { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Description,
            srcType: typeof(string),
            trgName: WorkersCompensationClaimFields.Description,
            trgType: typeof(string))]
        public string? Description { get; set; }

        [Notification(OperationType.Create, "Regarding Record", 3, false, isHtml: true)]
        [Notification(OperationType.UpdateOwner, "Regarding Record", 3, false, isHtml: true)]
        public string? RegardingRecordDisplayLink { get; set; } 

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StatusReason,
            srcType: typeof(OptionSetValue),
            trgName: WorkersCompensationClaimFields.StatusReason,
            trgType: typeof(OptionSetValue))]
        public int? StatusReason { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ActivityStatus,
            srcType: typeof(OptionSetValue),
            trgName: WorkersCompensationClaimFields.ActivityStatus,
            trgType: typeof(OptionSetValue))]
        public int? ActivityStatus { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Regarding,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public string? RegardingName { get; set; }

        [DataverseReference(
            targetEntity: EntityLogicalNames.SafetyIncident
        )]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Regarding,
            srcType: typeof(EntityReference),
            trgName: WorkersCompensationClaimFields.Regarding,
            trgType: typeof(EntityReference))]
        public Guid RegardingId { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Regarding,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public EntityReference? Regarding { get; set; }
        public override string LinkToRecord(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName)
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
            string targetAppDisplayName)
        {
            return base.GenerateSummaryTable(
                context,
                entityLogicalName,
                entityId,
                targetAppDisplayName);
        }
    }
}