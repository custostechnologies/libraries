using System;
using System.Diagnostics.Contracts;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Attributes;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Enums;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class JobAssignmentDto : IQueryDto
    {
        
        [DataverseLink(srcName: CoreJobAssignmentFields.StaffId, srcType: typeof(int), trgName: null, trgType: null)]
        public int StaffId { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.CoreJobAssignmentId, srcType: typeof(Guid), trgName: null, trgType: null)]
        public Guid CoreJobAssignmentId { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.Name, srcType: typeof(string), trgName: null, trgType: null)]
        public string? Name  { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.StaffJobPositionSeq, srcType: typeof(int), trgName: null, trgType: null)]
        public int StaffJobPositionSeq  { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.CoreJobPosition, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public Guid? CoreJobPositionId { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.CoreJobPosition, srcType: typeof(EntityReference), trgName: null, trgType: null)]
        public string? CoreJobPositionName { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.CoreStaffMember, 
            srcType: typeof(EntityReference), 
            trgName: null, 
            trgType: null)]
        public string? CoreStaffMemberName { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.CoreStaffMember, 
            srcType: typeof(EntityReference), 
            trgName: null, 
            trgType: null)]
        public Guid? CoreStaffMemberId { get; set; }

        [DataverseLink(
            srcName: $"staff.{CoreStaffMemberFields.AzureObjectId}", 
            srcType: typeof(Guid), 
            trgName: null, 
            trgType: null)]
        public Guid? AzureObjectId { get; set; }

        [DataverseLink(
            srcName: $"staff.{CoreStaffMemberFields.WorkEmail}", 
            srcType: typeof(string), 
            trgName: null, 
            trgType: null)]
        public string? WorkEmail { get; set; }
        
        [DataverseLink(srcName: CoreJobAssignmentFields.IsActive, srcType: typeof(bool), trgName: null, trgType: null)]
        public bool IsActive { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.SynReportsTo, srcType: typeof(string), trgName: null, trgType: null)]
        public string? SynReportsTo { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.Status, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public int Status { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.Status, srcType: typeof(OptionSetValue), trgName: null, trgType: null)]
        public string? StatusLabel { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.StartDate, srcType: typeof(DateTime), trgName: null, trgType: null)]
        public DateTime? StartDate { get; set; }

        [DataverseLink(srcName: CoreJobAssignmentFields.EndDate, srcType: typeof(DateTime), trgName: null, trgType: null)]
        public DateTime? EndDate { get; set; }
    }
}