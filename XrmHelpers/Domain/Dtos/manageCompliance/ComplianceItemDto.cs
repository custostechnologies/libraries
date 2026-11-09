using Microsoft.Xrm.Sdk;

using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Shared.Utilities;

public class ComplianceItemDto : TaskBaseDto
{
    public ComplianceItemDto() : base() { }
    protected override string RegardingEntityLogicalName =>
        EntityLogicalNames.ComplianceItem;
    protected override string NextDueDateName =>
        ComplianceItemFields.NextDueDate;
    protected override string NextReviewDateName =>
        ComplianceItemFields.NextReviewDate;
    protected override string PreviousReviewDateName =>
        ComplianceItemFields.PreviousReviewDate;
    protected override TaskType TaskType =>
        ChildTaskType ?? TaskType.ComplianceAction;

    [IgnoreOnCreate]
    [DataverseLink(
        srcName: ComplianceItemFields.ComplianceItemId,
        srcType: typeof(Guid),
        trgName: null,
        trgType: null)]
    public Guid ComplianceItemId { get; set; }

    public override Guid EntityId
    {
        get => ComplianceItemId;
        set => ComplianceItemId = value;
    }

    [DataverseLink(
        srcName: ComplianceItemFields.Name,
        srcType: typeof(string),
        trgName: ComplianceItemFields.Name,
        trgType: typeof(string))]
    public string? Name { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.Key,
        srcType: typeof(string),
        trgName: ComplianceItemFields.Key,
        trgType: typeof(string))]
    public string? Key { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.Description,
        srcType: typeof(string),
        trgName: ComplianceItemFields.Description,
        trgType: typeof(string))]
    public string? Description { get; set; }

    public override string Subject
    {
        get
        {
            var type = EnumExtensions.ToDescription(
                (int)(ChildTaskType ?? TaskType.Other),
                typeof(TaskType)
            );

            var subject = $"{type}: {Name}";
            return subject;
        }
    }

    public override string? TaskDescription =>
        TaskSummary ?? Description ?? "See Regarding Record";

    [DataverseLink(
        srcName: ComplianceItemFields.TaskSummary,
        srcType: typeof(string),
        trgName: ComplianceItemFields.TaskSummary,
        trgType: typeof(string))]
    public string? TaskSummary { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.TextSourceOfObligation,
        srcType: typeof(string),
        trgName: ComplianceItemFields.TextSourceOfObligation,
        trgType: typeof(string))]
    public string? TextSourceOfObligation { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.TextPeopleResponsible,
        srcType: typeof(string),
        trgName: ComplianceItemFields.TextPeopleResponsible,
        trgType: typeof(string))]
    public string? TextPeopleResponsible { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.ResponsibleTeams,
        srcType: typeof(string),
        trgName: ComplianceItemFields.ResponsibleTeams,
        trgType: typeof(string))]
    public string? ResponsibleTeams { get; set; }

    [EnumType(typeof(ComplianceCategory))]
    [DataverseLink(
        srcName: ComplianceItemFields.Category,
        srcType: typeof(OptionSetValue),
        trgName: ComplianceItemFields.Category,
        trgType: typeof(OptionSetValue))]
    public ComplianceCategory? Category { get; set; }

    [EnumType(typeof(ComplianceSubCategory))]
    [DataverseLink(
        srcName: ComplianceItemFields.SubCategory,
        srcType: typeof(OptionSetValue),
        trgName: ComplianceItemFields.SubCategory,
        trgType: typeof(OptionSetValue))]
    public ComplianceSubCategory? SubCategory { get; set; }

    [EnumType(typeof(TaskType))]
    [DataverseLink(
        srcName: ComplianceItemFields.ChildTaskType,
        srcType: typeof(OptionSetValue),
        trgName: ComplianceItemFields.ChildTaskType,
        trgType: typeof(OptionSetValue))]
    public TaskType? ChildTaskType { get; set; }

    [EnumType(typeof(ComplianceFlag))]
    [DataverseLink(
        srcName: ComplianceItemFields.ComplianceFlag,
        srcType: typeof(OptionSetValue),
        trgName: ComplianceItemFields.ComplianceFlag,
        trgType: typeof(OptionSetValue))]
    public ComplianceFlag? ComplianceFlag { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.ReviewCycle,
        srcType: typeof(int?),
        trgName: ComplianceItemFields.ReviewCycle,
        trgType: typeof(int?))]
    public override int? ReviewCycle { get; set; }

    [EnumType(typeof(ReviewCycleUnit))]
    [DataverseLink(
        srcName: ComplianceItemFields.ReviewCycleUnits,
        srcType: typeof(OptionSetValue),
        trgName: ComplianceItemFields.ReviewCycleUnits,
        trgType: typeof(OptionSetValue))]
    public override ReviewCycleUnit? ReviewCycleUnits { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.NextReviewDate,
        srcType: typeof(DateTime?),
        trgName: ComplianceItemFields.NextReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextReviewDate { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.NextDueDate,
        srcType: typeof(DateTime?),
        trgName: ComplianceItemFields.NextDueDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextDueDate { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.PreviousReviewDate,
        srcType: typeof(DateTime?),
        trgName: ComplianceItemFields.PreviousReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? PreviousReviewDate { get; set; }

    [DataverseLink(
        srcName: ComplianceItemFields.RecurringSubTasks,
        srcType: typeof(bool?),
        trgName: ComplianceItemFields.RecurringSubTasks,
        trgType: typeof(bool?))]
    public override bool RecurringSubTasks { get; set; }
    public override DateTime CalculateNextReviewDate()
    {
        return base.CalculateNextReviewDate();
    }
    public override DateTime CalculateNextDueDate(DateTime nextReviewDate)
    {
        return base.CalculateNextDueDate(nextReviewDate);
    }
    public override Entity GetReviewMetadataUpdate()
    {
        return base.GetReviewMetadataUpdate();
    }
}