using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Dtos
{
    public class ReferenceHierarchyMapDto
    {
        public Guid ReferenceHierarchyMapId { get; set; }
        public EntityReference? ChildJobPosition { get; set; }
        public EntityReference? CoreJobPosition { get; set; }
        public OptionSetValue? LinkType { get; set; }
        public string? MapVersion { get; set; }
        public string? Name { get; set; }
        public EntityReference? ParentJobPosition { get; set; }
        public string? SourceOfTruthId { get; set; }
        public OptionSetValue? Status { get; set; }
        public OptionSetValue? StatusReason { get; set; }
    }
}