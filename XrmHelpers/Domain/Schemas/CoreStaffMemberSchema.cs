using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class CoreStaffMemberSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            CoreStaffMemberFields.AzureObjectId,
            CoreStaffMemberFields.SourceOfTruthId,
            CoreStaffMemberFields.StaffId,
            CoreStaffMemberFields.User,

            CoreStaffMemberFields.IsActive,
            CoreStaffMemberFields.Status,
            CoreStaffMemberFields.StatusReason,

            CoreStaffMemberFields.Campus,
            CoreStaffMemberFields.CoreDepartment,
            CoreStaffMemberFields.CoreDepartmentIdx,
            CoreStaffMemberFields.CoreStaffCategory,
            CoreStaffMemberFields.CoreStaffCategoryIdx,
            CoreStaffMemberFields.StartDate,
            CoreStaffMemberFields.EndDate,
            CoreStaffMemberFields.NetworkLogin,
            CoreStaffMemberFields.WorkEmail,

            CoreStaffMemberFields.DisplayName,
            CoreStaffMemberFields.FullName,
            CoreStaffMemberFields.Gender,
            CoreStaffMemberFields.PreferredName,
            CoreStaffMemberFields.GivenName1,
            CoreStaffMemberFields.GivenName2,
            CoreStaffMemberFields.LastName,
            CoreStaffMemberFields.PreviousLastName,
            CoreStaffMemberFields.MaidenName,
            CoreStaffMemberFields.PersonalEmail,

            CoreStaffMemberFields.InUserGroup,

            CoreStaffMemberFields.ProfileResolutionFlag,
            CoreStaffMemberFields.SPGSEmailAddressFlag,
            CoreStaffMemberFields.StaffEmailAddressFlag,

            CoreStaffMemberFields.LastSync
        );
    }
}