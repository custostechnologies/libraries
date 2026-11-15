using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class CoreStudentSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet SyncColumns = new ColumnSet
            (
                CoreStudentFields.CoreStudentId,
                CoreStudentFields.Status,
                CoreStudentFields.StatusReason,
                CoreStudentFields.Given1,
                CoreStudentFields.Given2,
                CoreStudentFields.Surname,
                CoreStudentFields.Preferred,
                CoreStudentFields.DisplayName,
                CoreStudentFields.AzureObjectId,
                CoreStudentFields.Age,
                CoreStudentFields.YearLevel,
                CoreStudentFields.Campus,
                CoreStudentFields.House
            );
    }
}