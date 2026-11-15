using custos.XrmHelpers.Domain.Dtos;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IHierarchyMetadataReponse
    {
        List<ReportsToHierarchyDto>? ReportsTo { get; init; }
        List<DirectReportsHierarchyDto>? DirectReports { get; init; }
    }
}