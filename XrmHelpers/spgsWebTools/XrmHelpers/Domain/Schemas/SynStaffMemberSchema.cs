using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class SynStaffMemberSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet SyncColumns = new ColumnSet
            (
                SynStaffMemberFields.StaffId,
                SynStaffMemberFields.StaffPreferredName,
                SynStaffMemberFields.StaffCampus,
                SynStaffMemberFields.CampusKey,
                SynStaffMemberFields.SchoolStaffCode,
                SynStaffMemberFields.StaffCategory,
                SynStaffMemberFields.StaffDepartment,
                SynStaffMemberFields.LeavingDate,
                SynStaffMemberFields.ActiveFlag,
                SynStaffMemberFields.StartDate,
                SynStaffMemberFields.SubSchool,
                SynStaffMemberFields.CommunityGuid,
                SynStaffMemberFields.Email,
                SynStaffMemberFields.Gender,
                SynStaffMemberFields.GenderKey,
                SynStaffMemberFields.Given1,
                SynStaffMemberFields.Given2,
                SynStaffMemberFields.MaidenName,
                SynStaffMemberFields.NameInternal,
                SynStaffMemberFields.NetworkLogin,
                SynStaffMemberFields.OccupEmail,
                SynStaffMemberFields.OccupPhone,
                SynStaffMemberFields.Surname,
                SynStaffMemberFields.SurnamePrevious,
                SynStaffMemberFields.SourceOfTruthId,
                SynStaffMemberFields.EncodedHash
            );

        /// <summary>
        /// 
        /// </summary>
        public static readonly HashSet<string> TrackedFields = new HashSet<string>
        {
            SynStaffMemberFields.StaffId,
                SynStaffMemberFields.StaffPreferredName,
                SynStaffMemberFields.StaffCampus,
                SynStaffMemberFields.CampusKey,
                SynStaffMemberFields.SchoolStaffCode,
                SynStaffMemberFields.StaffCategory,
                SynStaffMemberFields.StaffDepartment,
                SynStaffMemberFields.LeavingDate,
                SynStaffMemberFields.ActiveFlag,
                SynStaffMemberFields.StartDate,
                SynStaffMemberFields.SubSchool,
                SynStaffMemberFields.CommunityGuid,
                SynStaffMemberFields.Email,
                SynStaffMemberFields.Gender,
                SynStaffMemberFields.GenderKey,
                SynStaffMemberFields.Given1,
                SynStaffMemberFields.Given2,
                SynStaffMemberFields.MaidenName,
                SynStaffMemberFields.NameInternal,
                SynStaffMemberFields.NetworkLogin,
                SynStaffMemberFields.OccupEmail,
                SynStaffMemberFields.OccupPhone,
                SynStaffMemberFields.Surname,
                SynStaffMemberFields.SurnamePrevious,
                SynStaffMemberFields.SourceOfTruthId,
                "pgrowth_forceplugin"
        };
    }
}