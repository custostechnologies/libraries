using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// 
    /// </summary>
    public static class ThemSchema
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