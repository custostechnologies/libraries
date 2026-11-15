namespace custos.XrmHelpers.Constants
{
        public static class SynJobPositionFields
        {
                // Identity
                public const string Id = "pgrowth_synergetic_vstaffjobpositionsid";
                public const string RecordTitle = "pgrowth_recordtitle";
                public const string StaffId = "pgrowth_staffid";
                public const string SourceOfTruthId = "pgrowth_sourceoftruthid";

                // Job Position
                public const string JobPositionActiveFlag = "pgrowth_jobpositionactiveflag";
                public const string JobPositionCategoryCode = "pgrowth_jobpositioncategorycode";
                public const string JobPositionCategoryDescription = "pgrowth_jobpositioncategorydescription";
                public const string JobPositionDescription = "pgrowth_jobpositiondescription";
                public const string JobPositionFTERequired = "pgrowth_jobpositionfterequired";
                public const string JobPositionOverview = "pgrowth_jobpositionoverview";
                public const string JobPositionsSeq = "pgrowth_jobpositionsseq";
                public const string ReportsTo = "pgrowth_reportsto";
                public const string NextReviewDate = "pgrowth_nextreviewdate";

                // Staff Job Position
                public const string StaffJobPositionActiveFlag = "pgrowth_staffjobpositionactiveflag";
                public const string StaffJobPositionCode = "pgrowth_staffjobpositioncode";
                public const string StaffJobPositionStartDate = "pgrowth_staffjobpositionstartdate";
                public const string StaffJobPositionEndDate = "pgrowth_staffjobpositionenddate";
                public const string StaffJobPositionExistsFlag = "pgrowth_staffjobpositionexistsflag";
                public const string StaffJobPositionFTE = "pgrowth_staffjobpositionfte";
                public const string StaffJobPositionsSeq = "pgrowth_staffjobpositionsseq";

                // Staff
                public const string StaffNameInternal = "pgrowth_staffnameinternal";

                // Hash Values
                public const string EncodedHash = "pgrowth_encodedhash";
                public const string PlaintextHash = "pgrowth_plaintexthash";

                // Status
                public const string Status = "statecode";
                public const string StatusReason = "statuscode";

                // Sync
                public const string UtcNow = "pgrowth_utcnow";
                public const string ForcePlugin = "pgrowth_forceplugin";
                public const string MutePlugin = "pgrowth_muteplugin";
        }
}