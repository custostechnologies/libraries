using spgsWebTools.XrmHelpers.Domain.Dtos;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IHierarchyMetadataReponse
    {
        List<ReportsToHierarchyDto>? ReportsTo { get; init; }
        List<DirectReportsHierarchyDto>? DirectReports { get; init; }
    }
}