using System;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Domain.Attributes;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Enums;
using custos.XrmHelpers.Domain.Contracts;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace custos.XrmHelpers.Domain.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class DirectReportsHierarchyDto : IQueryDto
    {
        /*[DataverseLink(
            srcName: CoreJobAssignmentFields.CoreJobAssignmentId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? CoreJobAssignmentId { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.StaffId,
            srcType: typeof(int),
            trgName: null,
            trgType: null
        )]
        public int StaffId { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.StaffJobPositionSeq,
            srcType: typeof(int),
            trgName: null,
            trgType: null
        )]
        public int StaffJobPositionSeq { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.CoreStaffMember,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public Guid? CoreStaffMemberId { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.CoreStaffMember,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public string? CoreStaffMemberName { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.CoreJobPosition,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public Guid? CoreJobPositionId { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.CoreJobPosition,
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public string? CoreJobPositionName { get; set; }

        [DataverseLink(
            srcName: CoreJobAssignmentFields.IsActive,
            srcType: typeof(bool),
            trgName: null,
            trgType: null
        )]
        public bool IsActive { get; set; }*/

        /* LINK ENTITY: OPERATIONAL HIERARCHY MAPS */

        [DataverseLink(
            srcName: OperationalHierarchyMapFields.OperationalHierarchyMapId,
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? OperationalHierarchyMapId { get; set; }

        [DataverseLink(
            srcName: OperationalHierarchyMapFields.LinkType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null
        )]
        public LinkType LinkType { get; set; }

        [DataverseLink(
            srcName: OperationalHierarchyMapFields.LinkType,
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null
        )]
        public string? LinkTypeName { get; set; }

        [DataverseLink(
            srcName: OperationalHierarchyMapFields.ApprovalIndex,
            srcType: typeof(int),
            trgName: null,
            trgType: null
        )]
        public int ApprovalIndex { get; set; }

        /* LINK ENTITY -> CORE JOB ASSIGNMENTS [DIRECT REPORTS] */

        [DataverseLink(
            srcName: $"dr.{CoreJobAssignmentFields.CoreStaffMember}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public Guid? DirectReportCoreStaffMemberId { get; set; }

        [DataverseLink(
            srcName: $"dr.{CoreJobAssignmentFields.CoreStaffMember}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public string? DirectReportCoreStaffMemberName { get; set; }

        // azure id
        [DataverseLink(
            srcName: $"staff.{CoreStaffMemberFields.AzureObjectId}",
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? DirectReportAzureObjectId { get; set; }

        // staff id
        [DataverseLink(
            srcName: $"staff.{CoreStaffMemberFields.StaffId}",
            srcType: typeof(int),
            trgName: null,
            trgType: null
        )]
        public int? DirectReportStaffId { get; set; }

        //email
        [DataverseLink(
            srcName: $"dr.{CoreStaffMemberFields.WorkEmail}",
            srcType: typeof(string),
            trgName: null,
            trgType: null
        )]
        public string? DirectReportWorkEmail { get; set; }

        [DataverseLink(
            srcName: $"dr.{CoreJobAssignmentFields.CoreJobPosition}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public Guid? DirectReportCoreJobPositionId { get; set; }

        [DataverseLink(
            srcName: $"dr.{CoreJobAssignmentFields.CoreJobPosition}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public string? DirectReportCoreJobPositionName { get; set; }

    }
}