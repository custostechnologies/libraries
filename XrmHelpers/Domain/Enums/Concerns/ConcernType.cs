using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum ConcernType
    {
        [Description("Observation of Indicators")]
        ObservationOfIndicators = 311940000,

        [Description("Disclosure")]
        Disclosure = 311940001,

        [Description("Incident")]
        Incident = 311940002,

        [Description("Allegation")]
        Allegation = 311940003
    }
}