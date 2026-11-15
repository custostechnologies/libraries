using Microsoft.VisualBasic;
using Microsoft.Xrm.Sdk;

using Custos.XrmHelpers.Constants;
using Custos.XrmHelpers.Domain.Attributes;
using Custos.XrmHelpers.Domain.Contracts;
using Custos.XrmHelpers.Domain.Enums;
using Custos.XrmHelpers.Infrastructure.Dataverse;

namespace Custos.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// Dataverse Task notification DTO.
    /// </summary>
    public class TaskDto : NotifiableDto<TaskDto>, IQueryDto
    {
        public TaskDto() : base() { }

        public override string TargetAppDisplayName => "manageCompliance";

        public override string SourceEntityLogicalName => EntityLogicalNames.Task;

        public string RegardingEntityLogicalName => RegardingName ?? string.Empty;
        public Guid RegardingEntityId => RegardingId;

        [DataverseLink(
            srcName: TaskFields.TaskId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null)]
        public Guid TaskId { get; set; }

        public override Guid EntityId
        {
            get => TaskId;
            set => TaskId = value;
        }

        [Notification(
            operationType: OperationType.Create,
            label: "Subject",
            index: 0,
            isLink: true)]
        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Subject",
            index: 0,
            isLink: true)]
        [Notification(
            operationType: OperationType.SummaryNotification,
            label: "Subject",
            index: 0,
            isLink: true)]
        [DataverseLink(
            srcName: TaskFields.Subject,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Subject { get; set; }

        [DataverseLink(
            srcName: TaskFields.Description,
            srcType: typeof(string),
            trgName: null,
            trgType: null)]
        public string? Description { get; set; }

        [Notification(
            operationType: OperationType.Create,
            label: "Due Date",
            index: 1,
            isLink: false)]
        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Due Date",
            index: 1,
            isLink: false)]
        [Notification(
            operationType: OperationType.SummaryNotification,
            label: "Due Date",
            index: 1,
            isLink: false)]
        [DataverseLink(
            srcName: TaskFields.ScheduledEnd,
            srcType: typeof(DateTime?),
            trgName: null,
            trgType: null)]
        public DateTime? DueDate { get; set; }

        [Notification(
            operationType: OperationType.Create,
            label: "Priority",
            index: 2,
            isLink: false)]
        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Priority",
            index: 2,
            isLink: false)]
        [Notification(
            operationType: OperationType.SummaryNotification,
            label: "Priority",
            index: 2,
            isLink: false)]
        [DataverseLink(
            srcName: TaskFields.Priority,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public TaskPriority? Priority { get; set; }

        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Status",
            index: 3,
            isLink: false)]
        [DataverseLink(
            srcName: TaskFields.StatusReason,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public ActivityStatusReason? StatusReason { get; set; }

        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "% Complete",
            index: 4,
            isLink: false)]
        [DataverseLink(
            srcName: TaskFields.PercentComplete,
            srcType: typeof(int?),
            trgName: null,
            trgType: null)]
        public int? PercentComplete { get; set; }

        [Notification(
            operationType: OperationType.Create,
            label: "Category",
            index: 5,
            isLink: false)]
        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Category",
            index: 5,
            isLink: false)]
        [DataverseLink(
            srcName: TaskCustomFields.TaskCategory,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? TaskCategory { get; set; }
        
        [EnumType(typeof(TaskType))]
        [Notification(
            operationType: OperationType.Create,
            label: "Task Type",
            index: 6,
            isLink: false)]
        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Task Type",
            index: 6,
            isLink: false)]
        [DataverseLink(
            srcName: TaskCustomFields.TaskType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public TaskType? TaskType { get; set; }

        [Notification(
            operationType: OperationType.Create,
            label: "Compliance Flag",
            index: 7,
            isLink: false)]
        [Notification(
            operationType: OperationType.UpdateOwner,
            label: "Compliance Flag",
            index: 7,
            isLink: false)]
        [DataverseLink(
            srcName: TaskCustomFields.ComplianceFlag,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null)]
        public int? ComplianceFlag { get; set; }

        [DataverseLink(
            srcName: TaskFields.Owner,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public EntityReference? Owner { get; set; }

        [DataverseLink(
            srcName: TaskFields.Regarding,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public Guid RegardingId { get; set; }

        [DataverseLink(
            srcName: TaskFields.Regarding,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null)]
        public string? RegardingName { get; set; }

        [Notification(
            operationType: OperationType.SummaryNotification,
            label: "Days Until Due",
            index: 3,
            isLink: false)]
        public string DaysUntilDue => SetDaysUntilDue();
        
        private string SetDaysUntilDue()
        {
            var due = DueDate;
            if (due != null)
            {
                TimeSpan diff = (TimeSpan)(DateTime.Now - due);
                int wholeDays = diff.Days;

                if (due <= DateTime.Now)
                {
                    return $"Due in {wholeDays} days";
                }
                else
                {
                    return $"Overdue by {Math.Abs(wholeDays)} days";
                }
            }
            return "0";
        }

        public override string LinkToRecord(
            IDataverseContext context,
            string entityLogicalName,
            Guid entityId,
            string targetAppDisplayName
        )
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
            string targetAppDisplayName
        )
        {
            return base.GenerateSummaryTable(
                context,
                entityLogicalName,
                entityId,
                targetAppDisplayName);
        }
    }
}