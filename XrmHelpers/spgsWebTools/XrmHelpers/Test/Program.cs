using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Serialisation;

var services = new ServiceCollection();

services.AddSingleton<IValidationEngine, ValidationEngine>();
services.AddSingleton<IValidationRuleSetProvider<StudentDto>, StrictStudentRuleSet>();

var serviceProvider = services.AddXrmSerializationEngine().BuildServiceProvider();

Console.WriteLine("Start Dry Run...");

var student = new StudentDto
{
    StudentId = 283224,
    Given1 = "Matt"
};

var validationEngine = serviceProvider.GetRequiredService<IValidationEngine>();

ValidationResult report = validationEngine.Validate(student);

if (!report.IsValid)
{
    Console.WriteLine("\nERROR: Structural Payload Inconsistencies Found:");
    foreach (var error in report.Errors)
    {
        Console.WriteLine($"   -> [Field: {error.TargetAttribute}] Error: {error.Message}");
    }
}
else
{
    Console.WriteLine("\nPASS. Invoking core integration sync adapters...");
    // syncPipeline.ExecuteDataLayerSync(incomingRawStudent);
}