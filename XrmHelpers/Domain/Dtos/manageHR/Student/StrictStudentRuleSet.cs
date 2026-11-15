using custos.XrmHelpers.Domain.Contracts;

namespace custos.XrmHelpers.Domain.Dtos
{
    public class StrictStudentRuleSet : IValidationRuleSetProvider<StudentDto>
    {
        public string RuleSetName => "StrictStudentRules";
        public IEnumerable<IValidationRule<StudentDto>> GetRules()
        {
            yield return new Given1NotEmptyRule();
            yield return new NumericStudentIdRule();
            yield return new StudentIdMoreThanZeroRule();
            yield return new SurnameNotEmptyRule();
            yield return new HouseNotEmptyRule();
            yield return new GenderNotEmptyRule();
            yield return new CampusNotEmptyRule();
            yield return new AgeMoreThanZeroRule();
            yield return new ValidYearLevelRule();
        }
    }
}