namespace Custos.XrmHelpers.Domain.Contracts
{
    public interface IValidationRuleSetProvider<in T>
    {
        public string RuleSetName { get; }
        public IEnumerable<IValidationRule<T>> GetRules();
    }
}