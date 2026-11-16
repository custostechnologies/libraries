namespace Custos.XrmHelpers.Constants
{
    public static class RiskFields
    {
        public const string RiskId = "pgrowth_riskid";

        // Core
        public const string Name = "pgrowth_name";
        public const string Key = "pgrowth_key";
        public const string Classification = "pgrowth_classification";

        // Risk Assessment
        public const string RiskCategory = "pgrowth_riskcategory";
        public const string RiskSubCategory = "pgrowth_risksubcategory";
        public const string RiskType = "pgrowth_risktype";

        public const string RiskRating = "pgrowth_riskrating";
        public const string OverallRiskRating = "pgrowth_overallriskrating";

        public const string RiskLikelihoodRating = "pgrowth_risklikelihoodrating";
        public const string RiskConsequenceRating = "pgrowth_riskconsequencerating";
        public const string RiskControlRating = "pgrowth_riskcontrolrating";

        public const string RiskFlag = "pgrowth_riskflag";
        public const string RiskFlags = "pgrowth_riskflags";

        public const string RiskIdentifier = "pgrowth_riskidentifier";

        // Risk Details
        public const string RiskDescription = "pgrowth_riskdescription";
        public const string PotentialConsequences = "pgrowth_potentialconsequences";
        public const string RiskMitigationStrategy = "pgrowth_riskmitigationstrategy";

        // Legacy
        public const string RiskControlStrategy = "pgrowth_riskcontrolstrategy";
        public const string ReviewSchedule = "pgrowth_reviewschedule";

        // Review Metadata
        public const string LastReviewDate = "pgrowth_lastreviewdate";
        public const string NextReviewDate = "pgrowth_nextreviewdate";
        public const string NextDueDate = "mthomas_nextduedate";

        public const string ReviewCycleUnits = "mthomas_reviewcycleunits";
        public const string ReviewScheduleNum = "pgrowth_reviewschedulenum";

        public const string TaskSummary = "mthomas_tasksummary";

        // Reporting
        public const string ReportingMethod = "pgrowth_reportingmethod";

        // Microsoft / Solution Fields
        public const string DataState = "msft_datastate";

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