using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

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
    public class WorkersCompensationClaimDto : NotifiableDto<WorkersCompensationClaimDto>, IValidationMetadata, IQueryDto
    {
        public WorkersCompensationClaimDto() : base() { }

        public override string TargetAppDisplayName => "manageCompliance";

        public override string SourceEntityLogicalName => EntityLogicalNames.WorkersCompensationClaim;

        [IgnoreOnCreate]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ActivityId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null)]
        public Guid ActivityId { get; set; }

        public override Guid EntityId
        {
            get => ActivityId;
            set => ActivityId = value;
        }

        [DataverseLink(
            srcName: SynStaffMemberFields.ForcePlugin,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public override DateTime? ForcePlugin { get; set; }

        [Notification(OperationType.Create, "Claim Number", 0, false)]
        [Notification(OperationType.UpdateOwner, "Claim Number", 0, false)]
        [Notification(OperationType.SummaryNotification, "Claim Number", 0, false)]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ClaimNumber,
            srcType: typeof(int?),
            trgName: null,
            trgType: null)]
        public int? ClaimNumber { get; set; }

        [Notification(OperationType.Create, "Staff Member", 1, false)]
        [Notification(OperationType.UpdateOwner, "Staff Member", 1, false)]
        [Notification(OperationType.SummaryNotification, "Staff Member", 1, false)]
        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StaffMemberName,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? StaffMemberName { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StaffMemberEmail,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? StaffMemberEmail { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ClaimDescription,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? ClaimDescription { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.EmailMessageToEmployee,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? EmailMessageToEmployee { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.OtherKnownEmployment,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? OtherKnownEmployment { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.PaymentsInformationReceived,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? PaymentsInformationReceived { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.LostTimeClaim,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? LostTimeClaim { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ExpensesApproved,
            srcType: typeof(bool?),
            trgName: null,
            trgType: null)]
        public bool? ExpensesApproved { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StaffType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public PersonType? StaffType { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.RelatedUser,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public string? RelatedUserName { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.RelatedUser,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public Guid RelatedUserId { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Subject,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Subject { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.Description,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Description { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.StatusReason,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? StatusReason { get; set; }

        [DataverseLink(
            srcName: WorkersCompensationClaimFields.ActivityStatus,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? ActivityStatus { get; set; }

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