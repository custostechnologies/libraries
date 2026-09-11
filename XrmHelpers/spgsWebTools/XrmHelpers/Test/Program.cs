using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Serialisation;

var serviceProvider = new ServiceCollection().AddXrmSerializationEngine().BuildServiceProvider();

Console.WriteLine("Start Dry Run...");

var student = new StudentDto
{
    StudentId = -283224,
    Given1 = ""
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