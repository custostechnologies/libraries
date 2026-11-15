namespace custos.XrmHelpers.Constants
{
        public static class CoreJobPositionFields
        {
                // Identity
                public const string CoreJobPositionId = "pgrowth_corejobpositionid";
                public const string JobTitle = "pgrowth_jobtitle";
                public const string SourceOfTruthId = "pgrowth_sourceoftruthid";
                public const string JobPositionSeq = "mthomas_jobpositionseq";

                // Job Details
                public const string Code = "mthomas_code";
                public const string Category = "mthomas_category";
                public const string CategoryCode = "mthomas_categorycode";
                public const string CmeaType = "pgrowth_cmeatype";
                public const string PAOSSubType = "pgrowth_paossubtype";
                public const string ReportsTo = "pgrowth_reportsto";

                // Status
                public const string IsActive = "pgrowth_isactive";
                public const string Status = "statecode";
                public const string StatusReason = "statuscode";

                // Metadata
                public const string FteRequired = "pgrowth_fterequired";
                public const string NextReviewDate = "mthomas_nextreviewdate";
                public const string HierarchyDependsOnCampus = "pgrowth_hierarchydependsoncampus";
        }
}