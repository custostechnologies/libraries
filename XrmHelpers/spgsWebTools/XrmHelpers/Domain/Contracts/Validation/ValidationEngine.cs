namespace spgsWebTools.XrmHelpers.Domain.Contracts
{
     public class ValidationEngine : IValidationEngine
     {
          private readonly IServiceProvider _serviceProvider;
          public ValidationEngine(
               IServiceProvider serviceProvider
          )
          {
               _serviceProvider = serviceProvider;
          }
          public ValidationResult Validate<T>(T payload) where T : IValidationMetadata
          {
               var result = new ValidationResult();
               var ruleSet = _serviceProvider.GetServices<IValidationRuleSetProvider<T>>().FirstOrDefault();

               if (ruleSet == null)
               {
                    result.Errors.Add(new ValidationError(
                         typeof(payload).Name,
                         $"No ruleset registered for object of type: {typeof(T).Name}"
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