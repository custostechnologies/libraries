using System.Text;
using Custos.XrmHelpers.Infrastructure.Dataverse.Services;

namespace Custos.XrmHelpers.Domain.Contracts
{
    public sealed class DigestMetric
    {
        public string Label { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;
    }
}