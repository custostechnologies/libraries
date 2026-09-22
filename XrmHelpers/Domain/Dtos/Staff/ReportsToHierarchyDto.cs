using System;
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
    public class ReportsToHierarchyDto : IQueryDto
    {
        [DataverseLink(
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
        public bool IsActive { get; set; }

        /* LINK ENTITY: OPERATIONAL HIERARCHY MAPS */

        [DataverseLink(
            srcName: $"map.{OperationalHierarchyMapFields.OperationalHierarchyMapId}",
            srcType: typeof(Guid),
            trgName: null,
            trgType: null
        )]
        public Guid? OperationalHierarchyMapId { get; set; }

        [DataverseLink(
            srcName: $"map.{OperationalHierarchyMapFields.LinkType}",
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null
        )]
        public LinkType LinkType { get; set; }

        [DataverseLink(
            srcName: $"map.{OperationalHierarchyMapFields.LinkType}",
            srcType: typeof(OptionSetValue),
            trgName: null,
            trgType: null
        )]
        public string? LinkTypeName { get; set; }

        [DataverseLink(
            srcName: $"map.{OperationalHierarchyMapFields.ApprovalIndex}",
            srcType: typeof(int),
            trgName: null,
            trgType: null
        )]
        public int ApprovalIndex { get; set; }

        /* LINK ENTITY -> CORE JOB ASSIGNMENTS [REPORTS TO] */

        [DataverseLink(
            srcName: $"rt.{CoreJobAssignmentFields.CoreStaffMember}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public Guid? ReportsToCoreStaffMemberId { get; set; }

        [DataverseLink(
            srcName: $"rt.{CoreJobAssignmentFields.CoreStaffMember}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public string? ReportsToCoreStaffMemberName { get; set; }

        [DataverseLink(
            srcName: $"rt.{CoreJobAssignmentFields.CoreJobPosition}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public Guid? ReportsToCoreJobPositionId { get; set; }

        [DataverseLink(
            srcName: $"rt.{CoreJobAssignmentFields.CoreJobPosition}",
            srcType: typeof(EntityReference),
            trgName: null,
            trgType: null
        )]
        public string? ReportsToCoreJobPositionName { get; set; }

    }
}