using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface ISyncPipeline<TDto>
    {
        Entity ExecuteInbound(Entity target, Entity? preImage);
        Entity ExecuteOutbound(Entity target);
    }
}