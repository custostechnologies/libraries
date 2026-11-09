using Microsoft.Xrm.Sdk;

using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public abstract class TaskBaseDto
    {
        protected TaskBaseDto() : base() { }
        public virtual Guid EntityId { get; set; }
        public abstract string RegardingEntityLogicalName { get; }
        protected abstract string PreviousReviewDateName { get; }
        protected abstract string NextDueDateName { get; }
        protected abstract string NextReviewDateName { get; }
        public virtual string Subject { get; set; } = string.Empty;
        public virtual string? TaskDescription { get; set; } = string.Empty;
        public virtual int? ReviewCycle { get; set; }
        public virtual ReviewCycleUnit? ReviewCycleUnits { get; set; }
        public abstract TaskType TaskType { get; }
        public virtual DateTime? NextReviewDate { get; set; }
        public virtual DateTime? NextDueDate { get; set; }
        public virtual DateTime? PreviousReviewDate { get; set; }
        public virtual bool RecurringSubTasks { get; set; }
        public virtual Entity GetReviewMetadataUpdate()
        {
            var update = new Entity(
                RegardingEntityLogicalName,
                EntityId
            );

            var nextReviewDate = CalculateNextReviewDate();

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

            var date = NextReviewDate ?? DateTime.UtcNow;
            var period = ReviewCycle ?? 1;

            return ReviewCycleUnits switch
            {
                ReviewCycleUnit.Days => date.AddDays(period),
                ReviewCycleUnit.Months => date.AddMonths(period),
                ReviewCycleUnit.Years => date.AddYears(period),
                _ => date.AddDays(period)
            };
        }
        public virtual DateTime CalculateNextDueDate(DateTime nextReviewDate)
        {
            var period = 30;

            return nextReviewDate.AddDays(period);
        }
    }
}