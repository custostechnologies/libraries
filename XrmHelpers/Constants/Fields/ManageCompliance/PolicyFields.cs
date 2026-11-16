namespace Custos.XrmHelpers.Constants
{
    public static class PolicyFields
    {
        public const string PolicyId = "pgrowth_policyid";

        // Core
        public const string Title = "pgrowth_title";
        public const string Description = "pgrowth_description";
        public const string DescriptionOfResponsibility = "pgrowth_descriptionofresponsibility";

        // Classification
        public const string Category = "pgrowth_category";
        public const string Type = "pgrowth_type";
        public const string RecordType = "pgrowth_recordtype";
        public const string Responsibility = "pgrowth_responsibility";
        public const string Location = "pgrowth_location";

        // Relationships
        public const string RelatedResponsibility = "pgrowth_relatedresponsibility";

        // URLs
        public const string LongUrl = "pgrowth_longurl";
        public const string ShortUrl = "pgrowth_shorturl";
        public const string MyStPaulsUrl = "pgrowth_mystpaulsurl";
        public const string HostedOnSchoolWebsite = "pgrowth_hostedonschoolwebsite";

        // Review Metadata
        public const string LastApprovalDate = "pgrowth_lastapprovaldate";
        public const string LastReviewDate = "pgrowth_lastreviewdate";
        public const string NextReviewDate = "pgrowth_nextreviewdate";
        public const string NextDueDate = "mthomas_nextduedate";
        public const string ReviewCycle = "pgrowth_reviewcycle";
        public const string ReviewCycleUnits = "pgrowth_reviewcycleunits";
        public const string RecurringSubTasks = "mthomas_recurringsubtasks";
        public const string TaskSummary = "mthomas_tasksummary";

        // Migration
        public const string Migrate = "pgrowth_migrate";

        // Ownership
        public const string Owner = "ownerid";
        public const string OwningBusinessUnit = "owningbusinessunit";
        public const string OwningTeam = "owningteam";
        public const string OwningUser = "owninguser";

        // System Fields
        public const string CreatedBy = "createdby";
        public const string CreatedByDelegate = "createdonbehalfby";
        public const string CreatedOn = "createdon";

        public const string ModifiedBy = "modifiedby";
        public const string ModifiedByDelegate = "modifiedonbehalfby";
        public const string ModifiedOn = "modifiedon";

        public const string ImportSequenceNumber = "importsequencenumber";
        public const string OverriddenCreatedOn = "overriddencreatedon";

        public const string Status = "statecode";
        public const string StatusReason = "statuscode";

        public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
        public const string UtcConversionTimeZoneCode = "utcconversiontimezonecode";
        public const string VersionNumber = "versionnumber";
    }
}