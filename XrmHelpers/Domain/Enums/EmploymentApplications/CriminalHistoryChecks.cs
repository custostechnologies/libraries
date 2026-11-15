using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum CriminalHistoryChecks
    {
        [Description("No Sexual Drug Offences")]
        NoSexualDrugOffences = 386630000,
        [Description("No Other Criminal Convictions")]
        NoOtherCriminalConvictions = 386630001,
        [Description("No Sexual Assault Charges NSW Crimes Act")]
        NoSexualAssaultChargesNswCrimesAct = 386630002,
        [Description("No Sexual Assault Charges Other Acts")]
        NoSexualAssaultChargesOtherActs = 386630003,
        [Description("No Unheard Charges")]
        NoUnheardCharges = 386630004,
        [Description("No Sexual Misconduct Disciplinary Action")]
        NoSexualMisconductDisciplinaryAction = 386630005
    }
}