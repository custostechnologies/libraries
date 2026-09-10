using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class SynStudentSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet SyncColumns = new ColumnSet
            (
                SynStudentFields.SynMirrorId,
                SynStudentFields.StudentActiveFlag,
                SynStudentFields.StudentGiven1,
                SynStudentFields.StudentGiven2,
                SynStudentFields.StudentSurname,
                SynStudentFields.StudentPreferred,
                SynStudentFields.Age,
                SynStudentFields.StudentCampus,
                SynStudentFields.StudentHouse,
                SynStudentFields.StudentYearLevel
            );

        /// <summary>
        /// 
        /// </summary>
        public static readonly HashSet<string> TrackedFields = new HashSet<string>
        {
                SynStudentFields.SynMirrorId,
                SynStudentFields.StudentActiveFlag,
                SynStudentFields.StudentGiven1,
                SynStudentFields.StudentGiven2,
                SynStudentFields.StudentSurname,
                SynStudentFields.StudentPreferred,
                SynStudentFields.Age,
                SynStudentFields.StudentCampus,
                SynStudentFields.StudentHouse,
                SynStudentFields.StudentYearLevel
        };
    }
}