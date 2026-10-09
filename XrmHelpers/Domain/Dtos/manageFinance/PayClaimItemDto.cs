using System;
using spgsWebTools.XrmHelpers.Constants;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    public class PayClaimItemDto : IQueryDto
    {

        [DataverseLink(srcName: PayClaimItemFields.PayClaimItemId, srcType: typeof(Guid), trgName: null, trgType: null)]
        public Guid? PayClaimItemId { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Status, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int? Status { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Status, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? StatusLabel { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.StatusReason, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int? StatusReason { get; set; }

        /*[DataverseLink(srcName: PayClaimItemFields.StatusReason, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? StatusReasonLabel { get; set; }*/

        [DataverseLink(srcName: PayClaimItemFields.AMDuty, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? AMDuty { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.BreakLength, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? BreakLength { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.CalcDailyClockedHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? CalcDailyClockedHours { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.CalcDailyPayableHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? CalcDailyPayableHours { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.ChildId, srcType: typeof(string), trgName: null, trgType: null)]
        public string? ChildId { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.CreationId, srcType: typeof(string), trgName: null, trgType: null)]
        public string? CreationId { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.DailyClockedHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? DailyClockedHours { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.DailyPayableHours, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? DailyPayableHours { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Date, srcType: typeof(DateTime), trgName: null, trgType: null)]
        public DateTime? Date { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Day, srcType: typeof(DateTime), trgName: null, trgType: null)]
        public DateTime? Day { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.DeprecatedTotalTimeWorked, srcType: typeof(decimal), trgName: null, trgType: null)]
        public decimal? DeprecatedTotalTimeWorked { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.FinishTime, srcType: typeof(DateTime), trgName: null, trgType: null)]
        public DateTime? FinishTime { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.LunchDuty, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? LunchDuty { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.PayClaimForm, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public Guid? PayClaimFormId { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.PayPeriod, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public Guid? PayPeriodId { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Period1, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? Period1 { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Period2, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? Period2 { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Period3, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? Period3 { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Period4, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? Period4 { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Period5, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? Period5 { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Period6, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? Period6 { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.PMDuty, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? PMDuty { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.Reason, srcType: typeof(string), trgName: null, trgType: null)]
        public string? Reason { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.RecessDuty, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool? RecessDuty { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.RecordTitle, srcType: typeof(string), trgName: null, trgType: null)]
        public string? RecordTitle { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.StartTime, srcType: typeof(DateTime), trgName: null, trgType: null)]
        public DateTime? StartTime { get; set; }

        [DataverseLink(srcName: PayClaimItemFields.WeekDay, srcType: typeof(string), trgName: null, trgType: null)]
        public string? WeekDay { get; set; }
    }
}