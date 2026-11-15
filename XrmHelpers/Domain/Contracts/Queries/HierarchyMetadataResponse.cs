using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public class HierarchyMetadataResponse : IHierarchyMetadataReponse
    {
        public List<ReportsToHierarchyDto>? ReportsTo { get; init; } = null;
        public List<DirectReportsHierarchyDto>? DirectReports { get; init; } = null;
    }
}