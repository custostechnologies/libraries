using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public abstract class TaskBaseDto : IDto
    {
        protected TaskBaseDto() : base() { }
        public virtual Guid EntityId { get; set; }
        public abstract string RegardingEntityLogicalName { get; }
        protected abstract string PreviousReviewDateName { get; }
        protected abstract string NextDueDateName { get; }
        protected abstract string NextReviewDateName { get; }
        public virtual string SourceEntityLogicalName { get; set; } = string.Empty;
        public virtual string TargetEntityLogicalName { get; set; } = string.Empty;
        public virtual KeyAttributeCollection? KeyCollection { get; }
        public virtual DateTime? ForcePlugin { get; set; } = null;
        public virtual string Subject { get; set; } = string.Empty;
        public virtual string? TaskDescription { get; set; } = string.Empty;
        public virtual int? ReviewCycle { get; set; }
        public virtual ReviewCycleUnit? ReviewCycleUnits { get; set; }
        public abstract TaskType TaskType { get; }
        public virtual DateTime? NextReviewDate { get; set; }
        public virtual DateTime? NextDueDate { get; set; }

        // TODO: set this when tasks are completed and item reverts to COMPLIANT
        public virtual DateTime? PreviousReviewDate { get; set; }
        public virtual bool RecurringSubTasks { get; set; }
        public virtual Entity GetReviewMetadataUpdate(DateTime nextReviewDate)
        {
            var update = new Entity(
                RegardingEntityLogicalName,
                EntityId
            );

            update[PreviousReviewDateName] = NextReviewDate;
            update[NextReviewDateName] = nextReviewDate;
            update[NextDueDateName] = CalculateNextDueDate(nextReviewDate);

            return update;
        }
        public virtual DateTime CalculateNextReviewDate()
        {
            if (ReviewCycleUnits is null)
            {
                throw new InvalidOperationException($"ReviewCycleUnits cannot be null");
            }

            var nextDate = (NextReviewDate ?? DateTime.UtcNow).Date;
            var period = ReviewCycle ?? 1;
            var today = DateTime.UtcNow.Date;

            while (nextDate <= today)
            {
                nextDate = ReviewCycleUnits switch
                {
                    ReviewCycleUnit.Days => nextDate.AddDays(period),
                    ReviewCycleUnit.Months => nextDate.AddMonths(period),
                    ReviewCycleUnit.Years => nextDate.AddYears(period),
                    _ => nextDate.AddDays(period)
                };
            }
            return nextDate;
        }
        public virtual DateTime CalculateNextDueDate(DateTime nextReviewDate)
        {
            var period = 30;

            return nextReviewDate.AddDays(period);
        }
    }
}