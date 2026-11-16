namespace Custos.XrmHelpers.Constants
{
    public static class HazardFields
    {
        public const string HazardId = "pgrowth_hazardid";

        // Core
        public const string Name = "pgrowth_name";
        public const string Description = "pgrowth_description";
        public const string Key = "pgrowth_key";

        // Risk Assessment
        public const string RiskCategory = "pgrowth_riskcategory";
        public const string RiskSubCategory = "pgrowth_risksubcategory";
        public const string RiskConsequence = "pgrowth_riskconsequence";
        public const string RiskLikelihood = "pgrowth_risklikelihood";
        public const string RiskRating = "pgrowth_riskrating";
        public const string RiskFlag = "pgrowth_riskflag";
        public const string ControlRating = "pgrowth_controlrating";

        // Risk Treatment
        public const string PotentialConsequences = "pgrowth_potentialconsequences";
        public const string RiskControlStrategy = "pgrowth_riskcontrolstrategy";
        public const string TextControlPolicy = "pgrowth_textcontrolpolicy";

        // Review Metadata
        public const string LastReviewDate = "pgrowth_lastreviewdate";
        public const string NextReviewDate = "pgrowth_nextreviewdate";
        public const string NextDueDate = "pgrowth_nextduedate";
        public const string ReviewCycle = "pgrowth_reviewcycle";
        public const string ReviewCycleUnits = "pgrowth_reviewcycleunits";
        public const string RecurringSubTasks = "pgrowth_recurringsubtasks";
        public const string TaskSummary = "pgrowth_tasksummary";

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