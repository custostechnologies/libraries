using Microsoft.Xrm.Sdk.Query;
using custos.XrmHelpers.Constants;

namespace custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class ThemeSchema
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly ColumnSet AllColumns = new ColumnSet(
            ThemeFields.ThemeId,
            ThemeFields.Name,
            ThemeFields.Primary,
            ThemeFields.Secondary,
            ThemeFields.Highlight,
            ThemeFields.Alternate,
            ThemeFields.Status,
            ThemeFields.StatusReason
        );
    }
}