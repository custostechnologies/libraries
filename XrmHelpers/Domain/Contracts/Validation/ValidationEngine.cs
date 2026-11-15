using Microsoft.Extensions.DependencyInjection;

namespace Custos.XrmHelpers.Domain.Contracts
{
     public class ValidationEngine : IValidationEngine
     {
          private readonly IServiceProvider _provider;
          public ValidationEngine(
               IServiceProvider serviceProvider
          )
          {
               _provider = serviceProvider;
          }
          public ValidationResult Validate<T>(T payload) where T : IValidationMetadata
          {
               var result = new ValidationResult();
               var ruleSet = _provider
                    .GetServices<IValidationRuleSetProvider<T>>()
                    .FirstOrDefault(
                         set => set.RuleSetName == payload.ValidationRuleSet
                    );

               if (ruleSet == null)
               {
                    result.Errors.Add(new ValidationError(
                         payload.GetType().Name,
                         $"No ruleset registered for object of type: {payload.GetType().Name}"
                    ));
                    return result;
               }

               foreach (var rule in ruleSet.GetRules())
               {
                    var outcome = rule.Validate(payload);
                    if (!outcome.IsValid)
                    {
                         result.Errors.AddRange(outcome.Errors);
                    }
               }

               return result;
          }
     }
}