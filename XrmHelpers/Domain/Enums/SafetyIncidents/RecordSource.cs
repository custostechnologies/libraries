using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum RecordSource
    {
        [Description("My Compliance")]
        MyCompliance = 311940000,
        [Description("Manage Compliance")]
        ManageCompliance = 311940001,
        [Description("Manual Import")]
        ManualImport = 311940002
    }
}