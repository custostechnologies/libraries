namespace Custos.XrmHelpers.Constants
{
    public static class TaskFields
    {
        // Identity
        public const string TaskId = "activityid";

        // Core
        public const string Subject = "subject";
        public const string Description = "description";
        public const string Category = "category";
        public const string SubCategory = "subcategory";

        // Scheduling
        public const string ScheduledStart = "scheduledstart";
        public const string ScheduledEnd = "scheduledend";
        public const string ScheduledDuration = "scheduleddurationminutes";

        // Actuals
        public const string ActualStart = "actualstart";
        public const string ActualEnd = "actualend";
        public const string ActualDurationMinutes = "actualdurationminutes";

        // Status
        public const string ActivityStatus = "statecode";
        public const string StatusReason = "statuscode";
        public const string PercentComplete = "percentcomplete";

        // Priority
        public const string Priority = "prioritycode";

        // Ownership
        public const string Owner = "ownerid";
        public const string OwningUser = "owninguser";
        public const string OwningTeam = "owningteam";
        public const string OwningBusinessUnit = "owningbusinessunit";

        // Relationships
        public const string Regarding = "regardingobjectid";

        // SLA
        public const string Sla = "slaid";
        public const string SlaInvoked = "slainvokedid";

        // Audit
        public const string CreatedOn = "createdon";
        public const string CreatedBy = "createdby";
        public const string ModifiedOn = "modifiedon";
        public const string ModifiedBy = "modifiedby";

        // Misc
        public const string SortDate = "sortdate";
        public const string ActivityType = "activitytypecode";
        public const string IsWorkflowCreated = "isworkflowcreated";
        public const string IsRegularActivity = "isregularactivity";
        public const string IsBilled = "isbilled";
    }
    public static class TaskCustomFields
    {
        public const string AdobeSignAgreementId = "pgrowth_adobesignagreementid";

        public const string ComplianceFlag = "pgrowth_complianceflag";
        public const string ComplianceItem = "pgrowth_complianceitem";

        public const string RecurrenceCycle = "pgrowth_recurrencecycle";

        public const string TaskCategory = "pgrowth_taskcategory";
        public const string TaskSubCategory = "pgrowth_tasksubcategory";
        public const string TaskType = "pgrowth_tasktype";

        public const string TaskCreationFlowStatus =
            "mthomas_taskcreationflowstatus";
    }
}