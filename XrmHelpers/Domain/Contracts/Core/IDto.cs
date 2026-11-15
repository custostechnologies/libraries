using Microsoft.Xrm.Sdk;
using custos.XrmHelpers.Domain.Enums;

namespace custos.XrmHelpers.Domain.Contracts
{
    public interface IDto
    {
        string SourceEntityLogicalName { get; }
        string TargetEntityLogicalName { get; }
        DateTime? ForcePlugin { get; set; }
        KeyAttributeCollection? KeyCollection { get; }
    }
}