using Microsoft.Xrm.Sdk;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IReviewQuery
    {
        EntityCollection GetItemsPendingTaskAssignment();
    }
}