using Microsoft.Xrm.Sdk;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class OperationalHierarchyMapDto
    {
        public Guid OperationalHierarchyMapId { get; set; }

        public EntityReference? ChildJobAssignment { get; set; }
        public string? ChildJobAssignmentIdx { get; set; }

        public EntityReference? ChildJobPosition { get; set; }
        public string? ChildJobPositionIdx { get; set; }

        public EntityReference? CoreJobAssignment { get; set; }
        public string? CoreJobAssignmentIdx { get; set; }

        public EntityReference? CoreJobPosition { get; set; }
        public string? CoreJobPositionIdx { get; set; }

        public string? CreatedFromStaffJobPositionSeq { get; set; }

        public bool IsActive { get; set; }

        public OptionSetValue? LinkType { get; set; }

        public string? Name { get; set; }

        public EntityReference? ParentJobAssignment { get; set; }
        public string? ParentJobAssignmentIdx { get; set; }

        public EntityReference? ParentJobPosition { get; set; }
        public string? ParentJobPositionIdx { get; set; }

        public string? SourceOfTruthId { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }
    }
}