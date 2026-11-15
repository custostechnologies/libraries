using System.Text.Json;
using Microsoft.Xrm.Sdk;
namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IQueryMapper<TDto>
    {
        TDto MapToDto(Entity srcEntity);
        TDto MapJsonToDto(JsonElement json);
    }
}