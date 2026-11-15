using Microsoft.Xrm.Sdk;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IModel
    {
        string EntityLogicalName { get; }
    }
}