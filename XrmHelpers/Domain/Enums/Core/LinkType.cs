using System.ComponentModel;

namespace Custos.XrmHelpers.Domain.Enums
{
    public enum LinkType
    {
        [Description("Direct Line")]
        DirectLine = 384810000,
        [Description("Dotted Line")]
        DottedLine = 384810001,
        [Description("Functional Lead")]
        FunctionalLead = 384810002,
        [Description("Discipline Lead")]
        DisciplineLead = 384810003,
        [Description("Operational Lead")]
        OperationalLead = 384810004,
        [Description("Financial Lead")]
        FinancialLead = 384810005
    }
}