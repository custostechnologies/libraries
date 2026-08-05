namespace spgsWebTools.XrmHelpers.Constants
{
    public static class CoreJobAssignmentFields
    {
        // identity
        public const string CoreJobAssignmentId = "mthomas_corejobassignmentid";
        public const string Name = "mthomas_name";
        public const string SourceOfTruthId = "pgrowth_sourceoftruthid";
        public const string StaffJobPositionsSeq = "mthomas_StaffJobPositionsSeq";

        // job position
        public const string CoreJobPosition = "pgrowth_corejobposition";
        public const string CoreJobPositionIdx = "pgrowth_corejobpositionidx";
        public const string SynReportsTo = "mthomas_synreportsto";

        // user
        public const string CoreStaffMember = "pgrowth_corestaffmember";
        public const string CoreStaffMemberIdx = "pgrowth_corestaffmemberidx";
        public const string StaffId = "mthomas_staffid";

        // status
        public const string IsActive = "pgrowth_isactive";
        public const string Status = "statecode";
        public const string StatusReason = "statuscode";

        // metadata
        public const string AssignmentFte = "mthomas_assignmentfte";
        public const string FteRequired = "mthomas_fterequired";
        public const string StartDate = "mthomas_startdate";
        public const string EndDate = "mthomas_enddate";

        public const string LastSync = "pgrowth_lastsync";

    }
}