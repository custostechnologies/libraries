using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Enums;

public class HazardDto : TaskBaseDto, INotifiableDto
{
    public HazardDto() : base() { }

    public override string SourceEntityLogicalName =>
        EntityLogicalNames.Hazard;

    public override string RegardingEntityLogicalName =>
        EntityLogicalNames.Hazard;

    public override string TargetAppDisplayName =>
        "spgsWebTools";

    protected override string PreviousReviewDateName =>
        HazardFields.LastReviewDate;

    protected override string NextReviewDateName =>
        HazardFields.NextReviewDate;

    protected override string NextDueDateName =>
        HazardFields.NextDueDate;

    public override TaskType TaskType =>
        TaskType.HazardReview;

    [IgnoreOnCreate]
    [DataverseLink(
        srcName: HazardFields.HazardId,
        srcType: typeof(Guid),
        trgName: null,
        trgType: null)]
    public Guid HazardId { get; set; }

    public override Guid EntityId
    {
        get => HazardId;
        set => HazardId = value;
    }

    [DataverseLink(
        srcName: HazardFields.Name,
        srcType: typeof(string),
        trgName: HazardFields.Name,
        trgType: typeof(string))]
    public string? Name { get; set; }

    [DataverseLink(
        srcName: HazardFields.Key,
        srcType: typeof(string),
        trgName: HazardFields.Key,
        trgType: typeof(string))]
    public string? Key { get; set; }

    [DataverseLink(
        srcName: HazardFields.Description,
        srcType: typeof(string),
        trgName: HazardFields.Description,
        trgType: typeof(string))]
    public string? Description { get; set; }

    public override string Subject =>
        $"Hazard Review: {Name}";

    public override string? TaskDescription =>
        TaskSummary
        ?? Description
        ?? "See Regarding Record";

    [DataverseLink(
        srcName: HazardFields.TaskSummary,
        srcType: typeof(string),
        trgName: HazardFields.TaskSummary,
        trgType: typeof(string))]
    public override string? TaskSummary { get; set; }

    [DataverseLink(
        srcName: HazardFields.PotentialConsequences,
        srcType: typeof(string),
        trgName: HazardFields.PotentialConsequences,
        trgType: typeof(string))]
    public string? PotentialConsequences { get; set; }

    [DataverseLink(
        srcName: HazardFields.RiskControlStrategy,
        srcType: typeof(string),
        trgName: HazardFields.RiskControlStrategy,
        trgType: typeof(string))]
    public string? RiskControlStrategy { get; set; }

    [DataverseLink(
        srcName: HazardFields.TextControlPolicy,
        srcType: typeof(string),
        trgName: HazardFields.TextControlPolicy,
        trgType: typeof(string))]
    public string? TextControlPolicy { get; set; }

    [EnumType(typeof(RiskCategory))]
    [DataverseLink(
        srcName: HazardFields.RiskCategory,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.RiskCategory,
        trgType: typeof(OptionSetValue))]
    public RiskCategory? RiskCategory { get; set; }

    [EnumType(typeof(RiskSubCategory))]
    [DataverseLink(
        srcName: HazardFields.RiskSubCategory,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.RiskSubCategory,
        trgType: typeof(OptionSetValue))]
    public RiskSubCategory? RiskSubCategory { get; set; }

    [EnumType(typeof(RiskRating))]
    [DataverseLink(
        srcName: HazardFields.RiskRating,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.RiskRating,
        trgType: typeof(OptionSetValue))]
    public RiskRating? RiskRating { get; set; }

    [EnumType(typeof(RiskLikelihood))]
    [DataverseLink(
        srcName: HazardFields.RiskLikelihood,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.RiskLikelihood,
        trgType: typeof(OptionSetValue))]
    public RiskLikelihood? RiskLikelihood { get; set; }

    [EnumType(typeof(RiskConsequence))]
    [DataverseLink(
        srcName: HazardFields.RiskConsequence,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.RiskConsequence,
        trgType: typeof(OptionSetValue))]
    public RiskConsequence? RiskConsequence { get; set; }

    [EnumType(typeof(ControlRating))]
    [DataverseLink(
        srcName: HazardFields.ControlRating,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.ControlRating,
        trgType: typeof(OptionSetValue))]
    public ControlRating? ControlRating { get; set; }

    [EnumType(typeof(RiskFlag))]
    [DataverseLink(
        srcName: HazardFields.RiskFlag,
        srcType: typeof(OptionSetValueCollection),
        trgName: HazardFields.RiskFlag,
        trgType: typeof(OptionSetValueCollection))]
    public RiskFlag[]? RiskFlag { get; set; }

    [DataverseLink(
        srcName: HazardFields.ReviewCycle,
        srcType: typeof(int),
        trgName: HazardFields.ReviewCycle,
        trgType: typeof(int))]
    public override int? ReviewCycle { get; set; }

    [EnumType(typeof(ReviewCycleUnit))]
    [DataverseLink(
        srcName: HazardFields.ReviewCycleUnits,
        srcType: typeof(OptionSetValue),
        trgName: HazardFields.ReviewCycleUnits,
        trgType: typeof(OptionSetValue))]
    public override ReviewCycleUnit? ReviewCycleUnits { get; set; }

    [DataverseLink(
        srcName: HazardFields.NextReviewDate,
        srcType: typeof(DateTime?),
        trgName: HazardFields.NextReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextReviewDate { get; set; }

    [DataverseLink(
        srcName: HazardFields.NextDueDate,
        srcType: typeof(DateTime?),
        trgName: HazardFields.NextDueDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextDueDate { get; set; }

    [DataverseLink(
        srcName: HazardFields.LastReviewDate,
        srcType: typeof(DateTime?),
        trgName: HazardFields.LastReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? PreviousReviewDate { get; set; }

    [DataverseLink(
        srcName: HazardFields.RecurringSubTasks,
        srcType: typeof(bool),
        trgName: HazardFields.RecurringSubTasks,
        trgType: typeof(bool))]
    public override bool RecurringSubTasks { get; set; }

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