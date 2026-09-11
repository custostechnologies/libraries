using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    public class StrictStudentRuleSet : IValidationRuleSetProvider<StudentDto>
    {
        public string RuleSetName => "StrictStudentRules";
        public IEnumerable<IValidationRule<StudentDto>> GetRules()
        {
            yield return new Given1NotEmptyRule();
            yield return new NumericStudentIdRule();
        }
    }
}