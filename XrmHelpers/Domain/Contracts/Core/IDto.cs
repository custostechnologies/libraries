using Microsoft.Xrm.Sdk;

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