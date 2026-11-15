using System.ComponentModel;

namespace custos.XrmHelpers.Domain.Enums
{
    public enum StudentType
    {
        [Description("Junior School Student")]
        JuniorSchool = 311940000,

        [Description("Secondary School Student")]
        SecondarySchool = 311940001,
        
        [Description("Former Student")]
        FormerStudent = 311940002
    }
}