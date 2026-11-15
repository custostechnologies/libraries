using Microsoft.Xrm.Sdk;
using Custos.XrmHelpers.Domain.Enums;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IDto
    {
        string SourceEntityLogicalName { get; }
        string TargetEntityLogicalName { get; }
        DateTime? ForcePlugin { get; set; }
        KeyAttributeCollection? KeyCollection { get; }
    }
}