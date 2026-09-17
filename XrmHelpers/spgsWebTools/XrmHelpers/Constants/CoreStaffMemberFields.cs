namespace spgsWebTools.XrmHelpers.Constants
{
    public static class CoreStaffMemberFields
    {
        // Identity
        public const string AzureObjectId = "pgrowth_azureobjectid";
        public const string SourceOfTruthId = "pgrowth_sourceoftruthid";
        public const string StaffId = "pgrowth_staffid";
        public const string User = "pgrowth_user";
        public const string CoreStaffMemberId = "pgrowth_corestaffmemberid";

        // Profile Photo
        public const string Photo = "pgrowth_photo";
        public const string PhotoLastModified = "pgrowth_photolastmodified";
        public const string PhotoHash = "pgrowth_photohash";

        // Status
        public const string IsActive = "pgrowth_isactive";
        public const string Status = "statecode";
        public const string StatusReason = "statuscode";

        // Job metadata
        public const string Campus = "pgrowth_campus";
        public const string CoreDepartment = "pgrowth_coredepartment";
        public const string CoreDepartmentIdx = "pgrowth_coredepartmentidx";
        public const string CoreStaffCategory = "pgrowth_corestaffcategory";
        public const string CoreStaffCategoryIdx = "pgrowth_corestaffcategoryidx";
        public const string StartDate = "pgrowth_startdate";
        public const string EndDate = "pgrowth_enddate";
        public const string NetworkLogin = "pgrowth_networklogin";
        public const string WorkEmail = "pgrowth_workemail";

        // Personal details
        public const string DisplayName = "pgrowth_displayname";
        public const string FullName = "pgrowth_fullname";
        public const string Gender = "pgrowth_gender";
        public const string PreferredName = "pgrowth_preferredname";
        public const string GivenName1 = "pgrowth_givenname1";
        public const string GivenName2 = "pgrowth_givenname2";
        public const string LastName = "pgrowth_lastname";
        public const string PreviousLastName = "pgrowth_previouslastname";
        public const string MaidenName = "pgrowth_maidenname";
        public const string PersonalEmail = "pgrowth_personalemail";

        // Group membership
        public const string InUserGroup = "pgrowth_inusergroup";

        // Flags
        public const string ProfileResolutionFlag = "pgrowth_profileresolutionflag";
        public const string SPGSEmailAddressFlag = "pgrowth_spgsemailaddressflag";
        public const string StaffEmailAddressFlag = "pgrowth_staffemailflag";

        // Sync data
        public const string LastSync = "pgrowth_lastsync";
    }
}