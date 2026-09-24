using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IModel
    {
        string EntityLogicalName { get; }
    }
}