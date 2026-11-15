using Microsoft.Xrm.Sdk.Query;
using Custos.XrmHelpers.Constants;

namespace Custos.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Assignment schema used for synchronisation operations.
    /// </summary>
    public static class BusinessUnitSchema
    {
        public static readonly ColumnSet SyncColumns = new ColumnSet(
            BusinessUnitFields.BusinessUnitId,
            BusinessUnitFields.Description,
            BusinessUnitFields.IsDisabled,
            BusinessUnitFields.Name,
            BusinessUnitFields.Organisation,
            BusinessUnitFields.BUCode
        );
    }
}