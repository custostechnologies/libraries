using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum SyncOperations
    {
        [Description("Create")]
        Create = 1,
        [Description("Read")]
        Read = 2,
        [Description("Update")]
        Update = 3,
        [Description("Delete")]
        Delete = 4,
        [Description("Upsert")]
        Upsert = 5
    }
}