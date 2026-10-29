using Microsoft.Xrm.Sdk;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public interface IActivityDto
    {
        Guid RegardingId { get; }
        string RegardingRecordAppDisplayName { get; }
        string RegardingRecordEntityLogicalName { get; }
        string? RegardingName { get; }
        EntityReference? Regarding { get; set; }
        string? RegardingRecordDisplayLink { get; set; }
    }
}