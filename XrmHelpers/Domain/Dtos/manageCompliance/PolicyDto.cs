using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Enums;

public class PolicyDto : TaskBaseDto, INotifiableDto
{
    public PolicyDto() : base() { }

    public override string SourceEntityLogicalName =>
        EntityLogicalNames.Policy;

    public override string RegardingEntityLogicalName =>
        EntityLogicalNames.Policy;

    public override string TargetAppDisplayName =>
        "spgsWebTools";

    protected override string PreviousReviewDateName =>
        PolicyFields.LastReviewDate;

    protected override string NextReviewDateName =>
        PolicyFields.NextReviewDate;

    protected override string NextDueDateName =>
        PolicyFields.NextDueDate;

    public override TaskType TaskType =>
        TaskType.PolicyReview;

    [IgnoreOnCreate]
    [DataverseLink(
        srcName: PolicyFields.PolicyId,
        srcType: typeof(Guid),
        trgName: null,
        trgType: null)]
    public Guid PolicyId { get; set; }

    public override Guid EntityId
    {
        get => PolicyId;
        set => PolicyId = value;
    }

    [DataverseLink(
        srcName: PolicyFields.Title,
        srcType: typeof(string),
        trgName: PolicyFields.Title,
        trgType: typeof(string))]
    public string? Name { get; set; }

    [DataverseLink(
        srcName: PolicyFields.Description,
        srcType: typeof(string),
        trgName: PolicyFields.Description,
        trgType: typeof(string))]
    public string? Description { get; set; }

    [DataverseLink(
        srcName: PolicyFields.DescriptionOfResponsibility,
        srcType: typeof(string),
        trgName: PolicyFields.DescriptionOfResponsibility,
        trgType: typeof(string))]
    public string? DescriptionOfResponsibility { get; set; }

    public override string Subject =>
        $"Policy Review: {Name}";

    public override string? TaskDescription =>
        TaskSummary
        ?? Description
        ?? "See Regarding Record";

    [DataverseLink(
        srcName: PolicyFields.TaskSummary,
        srcType: typeof(string),
        trgName: PolicyFields.TaskSummary,
        trgType: typeof(string))]
    public override string? TaskSummary { get; set; }

    [EnumType(typeof(PolicyCategory))]
    [DataverseLink(
        srcName: PolicyFields.Category,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.Category,
        trgType: typeof(OptionSetValue))]
    public PolicyCategory? Category { get; set; }

    [EnumType(typeof(PolicyType))]
    [DataverseLink(
        srcName: PolicyFields.Type,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.Type,
        trgType: typeof(OptionSetValue))]
    public PolicyType? Type { get; set; }

    [DataverseLink(
        srcName: PolicyFields.RecordType,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.RecordType,
        trgType: typeof(OptionSetValue))]
    public int? RecordType { get; set; }

    [DataverseLink(
        srcName: PolicyFields.Responsibility,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.Responsibility,
        trgType: typeof(OptionSetValue))]
    public int? Responsibility { get; set; }

    [DataverseReference(EntityLogicalNames.Responsibility)]
    [DataverseLink(
        srcName: PolicyFields.RelatedResponsibility,
        srcType: typeof(EntityReference),
        trgName: PolicyFields.RelatedResponsibility,
        trgType: typeof(EntityReference))]
    public Guid? RelatedResponsibilityId { get; set; }

    [EnumType(typeof(PolicyLocation))]
    [DataverseLink(
        srcName: PolicyFields.Location,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.Location,
        trgType: typeof(OptionSetValue))]
    public PolicyLocation? Location { get; set; }

    [EnumType(typeof(Status))]
    [DataverseLink(
        srcName: PolicyFields.Status,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.Status,
        trgType: typeof(OptionSetValue))]
    public Status? Status { get; set; }

    [EnumType(typeof(PolicyStatusReason))]
    [DataverseLink(
        srcName: PolicyFields.StatusReason,
        srcType: typeof(OptionSetValue),
        trgName: PolicyFields.StatusReason,
        trgType: typeof(OptionSetValue))]
    public PolicyStatusReason? StatusReason { get; set; }

    [DataverseLink(
        srcName: PolicyFields.LongUrl,
        srcType: typeof(string),
        trgName: PolicyFields.LongUrl,
        trgType: typeof(string))]
    public string? LongUrl { get; set; }

    [DataverseLink(
        srcName: PolicyFields.ShortUrl,
        srcType: typeof(string),
        trgName: PolicyFields.ShortUrl,
        trgType: typeof(string))]
    public string? ShortUrl { get; set; }

    [DataverseLink(
        srcName: PolicyFields.MyStPaulsUrl,
        srcType: typeof(string),
        trgName: PolicyFields.MyStPaulsUrl,
        trgType: typeof(string))]
    public string? MyStPaulsUrl { get; set; }

    [DataverseLink(
        srcName: PolicyFields.HostedOnSchoolWebsite,
        srcType: typeof(bool),
        trgName: PolicyFields.HostedOnSchoolWebsite,
        trgType: typeof(bool))]
    public bool? HostedOnSchoolWebsite { get; set; }

    [DataverseLink(
        srcName: PolicyFields.LastApprovalDate,
        srcType: typeof(DateTime?),
        trgName: PolicyFields.LastApprovalDate,
        trgType: typeof(DateTime?))]
    public DateTime? LastApprovalDate { get; set; }

    [DataverseLink(
        srcName: PolicyFields.ReviewCycle,
        srcType: typeof(int),
        trgName: PolicyFields.ReviewCycle,
        trgType: typeof(int))]
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
        srcName: PolicyFields.LastReviewDate,
        srcType: typeof(DateTime?),
        trgName: PolicyFields.LastReviewDate,
        trgType: typeof(DateTime?))]
    public override DateTime? PreviousReviewDate { get; set; }

    [DataverseLink(
        srcName: PolicyFields.RecurringSubTasks,
        srcType: typeof(bool),
        trgName: PolicyFields.RecurringSubTasks,
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
        DateTime nextReviewDate,
        OptionSetValue updatedStatusReason
    )
    {
        return base.GetReviewMetadataUpdate(nextReviewDate, updatedStatusReason);
    }
}