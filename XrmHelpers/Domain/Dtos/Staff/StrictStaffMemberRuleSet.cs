using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Domain.Dtos
{
    public class StrictStaffMemberRuleSet : IValidationRuleSetProvider<StaffMemberDto>
    {
        public string RuleSetName => "StrictStaffMemberRuleSet";
        public IEnumerable<IValidationRule<StaffMemberDto>> GetRules()
        {
            yield return new Given1NotEmptyStaffRule();
            yield return new NumericStaffIdStaffRule();
            yield return new StaffIdMoreThanZeroStaffRule();
            yield return new SurnameNotEmptyStaffRule();
            yield return new CoreStaffCategoryNotEmptyStaffRule();
            yield return new GenderNotEmptyStaffRule();
            yield return new CampusNotEmptyStaffRule();
        }
    }
}