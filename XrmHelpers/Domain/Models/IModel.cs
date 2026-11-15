using Microsoft.Xrm.Sdk;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IModel
    {
        string EntityLogicalName { get; }
    }
}