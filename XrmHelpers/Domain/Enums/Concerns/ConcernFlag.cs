using System.ComponentModel;

namespace spgsWebTools.XrmHelpers.Domain.Enums
{
    public enum ConcernFlag
    {
        [Description("First Concern")]
        FirstConcern = 386630000,

        [Description("Previous Concerns Exist")]
        PreviousConcernsExist = 386630001
    }
}