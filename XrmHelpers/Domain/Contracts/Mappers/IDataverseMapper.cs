using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IDataverseMapper<TDto>
    {
        TDto MapToDto(Entity srcEntity);
        TDto MapJsonToDto(string payload);
        Entity MapToSource(TDto dto);
        Entity MapToTarget(TDto dto);
        Entity MapToSourceDelta(TDto dto, DeltaComparison<TDto> deltaComparison);
    }
}