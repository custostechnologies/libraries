using System.Runtime.Versioning;
using Custos.XrmHelpers.Domain.Contracts;

namespace Custos.XrmHelpers.Serialisation
{
    public class ValidationEngine<T> where T : IValidationMetadata
    {
        private readonly IEnumerable<object> _ruleSetProviders;
        public ValidationEngine(IEnumerable<object> ruleSetProviders)
        {
            _ruleSetProviders = ruleSetProviders;
        }
        public ValidationResult Validate(T payload)
        {
            Console.WriteLine($"[ValidationEngine] Loading configuration key for: '{payload.ValidationRuleSet}'");

            var result = new ValidationResult();

            // find provider strategy
            var provider = _ruleSetProviders
                .OfType<IValidationRuleSetProvider<T>>()
                .FirstOrDefault(p => p.RuleSetName.Equals(payload.ValidationRuleSet, StringComparison.OrdinalIgnoreCase));

            if (provider == null)
            {
                var error = new ValidationError
                (
                    "TargetAttribute",
                    $"Rule set provider mapping for '{payload.ValidationRuleSet}' not found."
                );
                result.Errors.Add(error);
                return result;
            }

            // Loop and process cleanly
            foreach (var rule in provider.GetRules())
            {
                var outcome = rule.Validate(payload);
                if (!outcome.IsValid) result.Errors.AddRange(outcome.Errors);
            }

            return result;
        }
    }
}