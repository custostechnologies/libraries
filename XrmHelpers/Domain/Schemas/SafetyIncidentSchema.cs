using Microsoft.Xrm.Sdk.Query;

using spgsWebTools.XrmHelpers.Constants;

namespace spgsWebTools.XrmHelpers.Domain.Schemas
{
    /// <summary>
    /// Core Job Assignment schema used for synchronisation operations.
    /// </summary>
    public static class SafetyIncidentSchema
    {
        public static readonly ColumnSet DigestColumns = new ColumnSet(
            SafetyIncidentFields.AutoGenTitle,
            SafetyIncidentFields.Campus,
            SafetyIncidentFields.IncidentType,
            SafetyIncidentFields.DateTimeOfIncident,
            SafetyIncidentFields.IncidentCategory,
            SafetyIncidentFields.IncidentTimeframe,
            SafetyIncidentFields.NameOfInjuredPerson,
            SafetyIncidentFields.SubmitterEmail,
            SafetyIncidentFields.SubmitterPosition,
            SafetyIncidentFields.SafetyIncidentId,
            SafetyIncidentFields.Status,
            SafetyIncidentFields.StatusReason,
            "ownerid"
        );
    }
}