using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class ReviewTaskDto : IDto
    {
        [IgnoreOnCreate]
        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.TaskId,
            trgType: typeof(Guid))]
        public Guid TaskId { get; set; }
        public string SourceEntityLogicalName => EntityLogicalNames.Task;
        public string TargetEntityLogicalName => EntityLogicalNames.Task;
        public KeyAttributeCollection? KeyCollection { get; } = null;
        
        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.Regarding,
            trgType: typeof(EntityReference))]
        public EntityReference? Regarding { get; set; }
        public string TargetAppDisplayName { get; set; } = string.Empty;

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.Owner,
            trgType: typeof(EntityReference))]
        public EntityReference? Owner { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskCustomFields.TaskType,
            trgType: typeof(OptionSetValue))]
        public TaskType TaskType { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.Priority,
            trgType: typeof(OptionSetValue))]
        public TaskPriority Priority { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.ScheduledEnd,
            trgType: typeof(DateTime?))]
        public DateTime? DueDate { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.Subject,
            trgType: typeof(string))]
        public string? Subject { get; set; }

        [DataverseLink(
            srcName: null,
            srcType: null,
            trgName: TaskFields.Description,
            trgType: typeof(string))]
        public string? Description { get; set; }
        public DateTime? ForcePlugin { get; set; } = null;
    }
}