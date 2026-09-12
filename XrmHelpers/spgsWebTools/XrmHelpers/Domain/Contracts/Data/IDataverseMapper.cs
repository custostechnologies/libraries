using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IDataverseMapper<TDto>
    {
        TDto MapToDto(Entity srcEntity) where TDto : new();
        Entity MapToSource(TDto dto);
        Entity MapToTarget(TDto dto);
    }
}