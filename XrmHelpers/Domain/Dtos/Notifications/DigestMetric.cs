using System.Text;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Services;

namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
    public sealed class DigestMetric
    {
        public string Label { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;
    }
}