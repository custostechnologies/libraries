using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Shared.Utilities;

public class PolicyDto : TaskBaseDto, INotifiableDto
{
    public PolicyDto() : base() { }
    public override string SourceEntityLogicalName =>
        EntityLogicalNames.Policy;
    public override string TargetAppDisplayName => "spgsWebTools";
    public override string RegardingEntityLogicalName =>
        EntityLogicalNames.Policy;
    protected override string NextDueDateName =>
        PolicyFields.NextDueDate;
    protected override string NextReviewDateName =>
        PolicyFields.NextReviewDate;
    protected override string PreviousReviewDateName =>
        PolicyFields.PreviousReviewDate;
    public override TaskType TaskType =>
        ChildTaskType ?? TaskType.PolicyReview;

    [IgnoreOnCreate]
    [DataverseLink(
        srcName: PolicyFields.ComplianceItemId,
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
        srcName: PolicyFields.Name,
        srcType: typeof(string),
        trgName: PolicyFields.Name,
        trgType: typeof(string))]
    public string? Name { get; set; }

    [DataverseLink(
        srcName: PolicyFields.Key,
        srcType: typeof(string),
        trgName: PolicyFields.Key,
        trgType: typeof(string))]
    public string? Key { get; set; }

    [DataverseLink(
        srcName: PolicyFields.Description,
        srcType: typeof(string),
        trgName: PolicyFields.Description,
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
        srcName: PolicyFields.TaskSummary,
        srcType: typeof(string),
        trgName: PolicyFields.TaskSummary,
        trgType: typeof(string))]
    public override string? TaskSummary { get; set; }

    [DataverseLink(
        srcName: PolicyFields.TextSourceOfObligation,
        srcType: typeof(string),
        trgName: PolicyFields.TextSourceOfObligation,
        trgType: typeof(string))]
    public string? TextSourceOfObligation { get; set; }

    [DataverseLink(
        srcName: PolicyFields.TextPeopleResponsible,
        srcType: typeof(string),
        trgName: PolicyFields.TextPeopleResponsible,
        trgType: typeof(string))]
    public string? TextPeopleResponsible { get; set; }

    [DataverseLink(
        srcName: PolicyFields.ResponsibleTeams,
        srcType: typeof(EntityReferenceCollection),
        trgName: PolicyFields.ResponsibleTeams,
        trgType: typeof(string))]
    public string? ResponsibleTeams { get; set; }

    [EnumType(typeof(ComplianceCategory))]
    [DataverseLink(
        srcName: PolicyFields.Category,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.Category,
        trgType: typeof(OptionSetValue))]
    public ComplianceCategory? Category { get; set; }

    [EnumType(typeof(ComplianceSubCategory))]
    [DataverseLink(
        srcName: PolicyFields.SubCategory,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.SubCategory,
        trgType: typeof(OptionSetValue))]
    public ComplianceSubCategory? SubCategory { get; set; }

    [EnumType(typeof(TaskType))]
    [DataverseLink(
        srcName: PolicyFields.ChildTaskType,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.ChildTaskType,
        trgType: typeof(OptionSetValue))]
    public TaskType? ChildTaskType { get; set; }

    [EnumType(typeof(ComplianceFlag))]
    [DataverseLink(
        srcName: PolicyFields.ComplianceFlag,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.ComplianceFlag,
        trgType: typeof(OptionSetValue))]
    public ComplianceFlag? ComplianceFlag { get; set; }

    [DataverseLink(
        srcName: PolicyFields.ReviewCycle,
        srcType: typeof(int?),
        trgName: PolicyFields.ReviewCycle,
        trgType: typeof(int?))]
    public override int? ReviewCycle { get; set; }

    [EnumType(typeof(ReviewCycleUnit))]
    [DataverseLink(
        srcName: PolicyFields.ReviewCycleUnits,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.ReviewCycleUnits,
        trgType: typeof(OptionSetValue))]
    public override ReviewCycleUnit? ReviewCycleUnits { get; set; }

    [DataverseLink(
        srcName: PolicyFields.NextReviewDate,
        srcType: typeof(DateTime?),
        trgName: PolicyFields.NextReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextReviewDate { get; set; }

    [DataverseLink(
        srcName: PolicyFields.NextDueDate,
        srcType: typeof(DateTime?),
        trgName: PolicyFields.NextDueDate,
        trgType: typeof(DateTime?))]
    public override DateTime? NextDueDate { get; set; }

    [DataverseLink(
        srcName: PolicyFields.PreviousReviewDate,
        srcType: typeof(DateTime?),
        trgName: PolicyFields.PreviousReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? PreviousReviewDate { get; set; }

    [DataverseLink(
        srcName: PolicyFields.RecurringSubTasks,
        srcType: typeof(bool?),
        trgName: PolicyFields.RecurringSubTasks,
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
    public override Entity GetReviewMetadataUpdate(DateTime nextReviewDate)
    {
        return base.GetReviewMetadataUpdate(nextReviewDate);
    }
}