using System.Text.Json;
using Microsoft.Xrm.Sdk;
namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IQueryMapper<TDto>
    {
        TDto MapToDto(Entity srcEntity);
        TDto MapJsonToDto(JsonElement json);
    }
}