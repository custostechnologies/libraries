using System;
using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Attributes;
using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Domain.Dtos
{
    public class PayPeriodDto : IQueryDto
    {
        [DataverseLink(
            srcName: PayPeriodFields.PayPeriodId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null)]
        public Guid PayPeriodId { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.Name,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Name { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.Status,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? Status { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.Status,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public string? StatusLabel { get; set; }

        /*[DataverseLink(
            srcName: PayPeriodFields.StatusReason,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? StatusReason { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.StatusReason,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public string? StatusReasonLabel { get; set; }*/

        [DataverseLink(
            srcName: PayPeriodFields.CasualCurrentFlag,
            srcType: typeof(bool),
            trgName: null,
            trgType: null)]
        public bool? CasualCurrentFlag { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.CasualPeriodStart,
            srcType: typeof(DateTime),
            trgName: null,
            trgType: null)]
        public DateTime? CasualPeriodStart { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.CasualPeriodEnd,
            srcType: typeof(DateTime),
            trgName: null,
            trgType: null)]
        public DateTime? CasualPeriodEnd { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.SalaryPeriodStart,
            srcType: typeof(DateTime),
            trgName: null,
            trgType: null)]
        public DateTime? SalaryPeriodStart { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.SalaryPeriodEnd,
            srcType: typeof(DateTime),
            trgName: null,
            trgType: null)]
        public DateTime? SalaryPeriodEnd { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.ProcessedDate,
            srcType: typeof(DateTime),
            trgName: null,
            trgType: null)]
        public DateTime? ProcessedDate { get; set; }

        [DataverseLink(
            srcName: PayPeriodFields.WeekNumber,
            srcType: typeof(int),
            trgName: null,
            trgType: null)]
        public int? WeekNumber { get; set; }
    }
}