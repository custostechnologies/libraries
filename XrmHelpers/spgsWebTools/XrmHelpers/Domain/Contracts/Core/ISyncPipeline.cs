using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface ISyncPipeline<TDto>
    {
        Entity Execute(Entity target, Entity preImage);
    }
}