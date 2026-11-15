using Custos.XrmHelpers.Domain.Dtos;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IHierarchyMetadataReponse
    {
        List<ReportsToHierarchyDto>? ReportsTo { get; init; }
        List<DirectReportsHierarchyDto>? DirectReports { get; init; }
    }
}