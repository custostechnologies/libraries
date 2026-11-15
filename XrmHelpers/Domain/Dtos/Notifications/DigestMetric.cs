using System.Text;
using custos.XrmHelpers.Infrastructure.Dataverse.Services;

namespace custos.XrmHelpers.Domain.Contracts
{
    public sealed class DigestMetric
    {
        public string Label { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;
    }
}