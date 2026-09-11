using spgsWebTools.XrmHelpers.Domain.Contracts;

namespace spgsWebTools.XrmHelpers.Serialisation
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
            var result = new ValidationResult();

            Console.WriteLine($"[ValidationEngine] Loading configuration key for: '{payload.ValidationRuleSet}'");

            // find provider strategy
            var provider = _ruleSetProviders
                .OfType<IValidationRuleSetProvider<T>>()
                .FirstOrDefault(p => p.RuleSetName.Equals(payload.ValidationRuleSet, StringComparison.OrdinalIgnoreCase));

            if (provider == null)
            {
                result.Errors.Add(new ValidationError
                {
                    Message = $"Rule set provider mapping for '{payload.ValidationRuleSet}' not found."
                });
                return result;
            }

            // Loop and process cleanly
            foreach (var rule in provider.GetRules())
            {
                if (!rule.IsValid(payload))
                {
                    result.Errors.Add(new ValidationError
                    {
                        Message = rule.ErrorMessage
                    });
                }
            }

            return result;
        }
    }
}