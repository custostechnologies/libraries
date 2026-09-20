using Microsoft.Xrm.Sdk;
using spgsWebTools.XrmHelpers.Domain.Enums;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IDto
    {
        string SourceEntityLogicalName { get; }
        string TargetEntityLogicalName { get; }
        DateTime? ForcePlugin { get; set; }
        KeyAttributeCollection? KeyCollection { get; }
    }
}