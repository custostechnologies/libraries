using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Enums;

public class RiskDto : TaskBaseDto, INotifiableDto
{
    public RiskDto() : base() { }

    public override string SourceEntityLogicalName =>
        EntityLogicalNames.Risk;

    public override string RegardingEntityLogicalName =>
        EntityLogicalNames.Risk;

    public override string TargetAppDisplayName =>
        "spgsWebTools";

    protected override string PreviousReviewDateName =>
        RiskFields.LastReviewDate;

    protected override string NextReviewDateName =>
        RiskFields.NextReviewDate;

    protected override string NextDueDateName =>
        RiskFields.NextDueDate;

    public override TaskType TaskType =>
        TaskType.RiskReview;

    [IgnoreOnCreate]
    [DataverseLink(
        srcName: RiskFields.RiskId,
        srcType: typeof(Guid),
        trgName: null,
        trgType: null)]
    public Guid RiskId { get; set; }

    public override Guid EntityId
    {
        get => RiskId;
        set => RiskId = value;
    }

    [DataverseLink(
        srcName: RiskFields.Name,
        srcType: typeof(string),
        trgName: RiskFields.Name,
        trgType: typeof(string))]
    public string? Name { get; set; }

    [DataverseLink(
        srcName: RiskFields.Key,
        srcType: typeof(string),
        trgName: RiskFields.Key,
        trgType: typeof(string))]
    public string? Key { get; set; }

    public override string Subject =>
        $"Risk Review: {Name}";

    public override string? TaskDescription =>
        TaskSummary
        ?? RiskDescription
        ?? "See Regarding Record";

    [DataverseLink(
        srcName: RiskFields.TaskSummary,
        srcType: typeof(string),
        trgName: RiskFields.TaskSummary,
        trgType: typeof(string))]
    public override string? TaskSummary { get; set; }

    [DataverseLink(
        srcName: RiskFields.RiskDescription,
        srcType: typeof(string),
        trgName: RiskFields.RiskDescription,
        trgType: typeof(string))]
    public string? RiskDescription { get; set; }

    [DataverseLink(
        srcName: RiskFields.PotentialConsequences,
        srcType: typeof(string),
        trgName: RiskFields.PotentialConsequences,
        trgType: typeof(string))]
    public string? PotentialConsequences { get; set; }

    [DataverseLink(
        srcName: RiskFields.RiskMitigationStrategy,
        srcType: typeof(string),
        trgName: RiskFields.RiskMitigationStrategy,
        trgType: typeof(string))]
    public string? RiskMitigationStrategy { get; set; }

    [DataverseLink(
        srcName: RiskFields.RiskIdentifier,
        srcType: typeof(string),
        trgName: RiskFields.RiskIdentifier,
        trgType: typeof(string))]
    public string? RiskIdentifier { get; set; }

    [EnumType(typeof(RiskCategory))]
    [DataverseLink(
        srcName: RiskFields.RiskCategory,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskCategory,
        trgType: typeof(OptionSetValue))]
    public RiskCategory? RiskCategory { get; set; }

    [EnumType(typeof(RiskSubCategory))]
    [DataverseLink(
        srcName: RiskFields.RiskSubCategory,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskSubCategory,
        trgType: typeof(OptionSetValue))]
    public RiskSubCategory? RiskSubCategory { get; set; }

    [EnumType(typeof(RiskRating))]
    [DataverseLink(
        srcName: RiskFields.RiskRating,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskRating,
        trgType: typeof(OptionSetValue))]
    public RiskRating? RiskRating { get; set; }

    [EnumType(typeof(RiskRating))]
    [DataverseLink(
        srcName: RiskFields.OverallRiskRating,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.OverallRiskRating,
        trgType: typeof(OptionSetValue))]
    public RiskRating? OverallRiskRating { get; set; }

    [EnumType(typeof(RiskLikelihood))]
    [DataverseLink(
        srcName: RiskFields.RiskLikelihoodRating,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskLikelihoodRating,
        trgType: typeof(OptionSetValue))]
    public RiskLikelihood? RiskLikelihoodRating { get; set; }

    [EnumType(typeof(RiskConsequence))]
    [DataverseLink(
        srcName: RiskFields.RiskConsequenceRating,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskConsequenceRating,
        trgType: typeof(OptionSetValue))]
    public RiskConsequence? RiskConsequenceRating { get; set; }

    [EnumType(typeof(ControlRating))]
    [DataverseLink(
        srcName: RiskFields.RiskControlRating,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskControlRating,
        trgType: typeof(OptionSetValue))]
    public ControlRating? RiskControlRating { get; set; }

    [EnumType(typeof(RiskFlag))]
    [DataverseLink(
        srcName: RiskFields.RiskFlag,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskFlag,
        trgType: typeof(OptionSetValue))]
    public RiskFlag? RiskFlag { get; set; }

    [EnumType(typeof(RiskFlag))]
    [DataverseLink(
        srcName: RiskFields.RiskFlags,
        srcType: typeof(OptionSetValueCollection),
        trgName: RiskFields.RiskFlags,
        trgType: typeof(OptionSetValueCollection))]
    public RiskFlag[]? RiskFlags { get; set; }

    [DataverseLink(
        srcName: RiskFields.Classification,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.Classification,
        trgType: typeof(OptionSetValue))]
    public int? Classification { get; set; }

    [DataverseLink(
        srcName: RiskFields.RiskType,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.RiskType,
        trgType: typeof(OptionSetValue))]
    public int? RiskType { get; set; }

    [DataverseLink(
        srcName: RiskFields.ReportingMethod,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.ReportingMethod,
        trgType: typeof(OptionSetValue))]
    public int? ReportingMethod { get; set; }

    [DataverseLink(
        srcName: RiskFields.ReviewScheduleNum,
        srcType: typeof(int),
        trgName: RiskFields.ReviewScheduleNum,
        trgType: typeof(int))]
    public override int? ReviewCycle { get; set; }

    [EnumType(typeof(ReviewCycleUnit))]
    [DataverseLink(
        srcName: RiskFields.ReviewCycleUnits,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.ReviewCycleUnits,
        trgType: typeof(OptionSetValue))]
    public override ReviewCycleUnit? ReviewCycleUnits { get; set; }

    [DataverseLink(
        srcName: RiskFields.NextReviewDate,
        srcType: typeof(DateTime?),
        trgName: RiskFields.NextReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextReviewDate { get; set; }

    [DataverseLink(
        srcName: RiskFields.NextDueDate,
        srcType: typeof(DateTime?),
        trgName: RiskFields.NextDueDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextDueDate { get; set; }

    [DataverseLink(
        srcName: RiskFields.LastReviewDate,
        srcType: typeof(DateTime?),
        trgName: RiskFields.LastReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? PreviousReviewDate { get; set; }

    [DataverseLink(
        srcName: RiskFields.Status,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.Status,
        trgType: typeof(OptionSetValue))]
    public int? Status { get; set; }

    [DataverseLink(
        srcName: RiskFields.StatusReason,
        srcType: typeof(OptionSetValue),
        trgName: RiskFields.StatusReason,
        trgType: typeof(OptionSetValue))]
    public int? StatusReason { get; set; }

    public override DateTime CalculateNextReviewDate()
    {
        return base.CalculateNextReviewDate();
    }

    public override DateTime CalculateNextDueDate(
        DateTime nextReviewDate
    )
    {
        return base.CalculateNextDueDate(nextReviewDate);
    }

    public override Entity GetReviewMetadataUpdate(
        DateTime nextReviewDate
    )
    {
        return base.GetReviewMetadataUpdate(nextReviewDate);
    }
}