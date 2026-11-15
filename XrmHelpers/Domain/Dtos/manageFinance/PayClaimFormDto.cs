using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Attributes;
using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Domain.Dtos
{
    public class PayClaimFormDto : IValidationMetadata, IQueryDto
    {
        public string ValidationRuleSet => "";

        [DataverseLink(srcName: PayClaimFormFields.PayClaimFormId, srcType: typeof(Guid), trgName: null, trgType: null)]
        public Guid? PayClaimFormId { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.ApprovalInformation, srcType: typeof(string), trgName: null, trgType: null)]
        public string? ApprovalInformation { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.CreationId, srcType: typeof(string), trgName: null, trgType: null)]
        public string? CreationId { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.FormType, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int? FormType { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.FormType, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? FormTypeLabel { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.PayPeriod, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public Guid? PayPeriodId { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.PayPeriod, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public string? PayPeriodName { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.RelatedApprovalId, srcType: typeof(string), trgName: null, trgType: null)]
        public string? RelatedApprovalId { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.RespondentType, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int? RespondentType { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.RespondentType, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? RespondentTypeLabel { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.RoleCategory, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public Guid? RoleCategoryId { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.RoleCategory, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public string? RoleCategoryName { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.SharePointExportPath, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SharePointExportPath { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.StaffMember, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public Guid? StaffMemberId { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.StaffMember, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public string? StaffMemberName { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.TargetEnvironmentAddress, srcType: typeof(string), trgName: null, trgType: null)]
        public string? TargetEnvironmentAddress { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.Title, srcType: typeof(string), trgName: null, trgType: null)]
        public string? Title { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.TotalClockedHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? TotalClockedHours { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.TotalPayableHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? TotalPayableHours { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.TotalUnpaidBreakHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? TotalUnpaidBreakHours { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.Status, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int? Status { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.Status, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? StatusLabel { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.StatusReason, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int? StatusReason { get; set; }

        [DataverseLink(srcName: PayClaimFormFields.StatusReason, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? PayClaimFormStatusReasonLabel { get; set; }
    }
}