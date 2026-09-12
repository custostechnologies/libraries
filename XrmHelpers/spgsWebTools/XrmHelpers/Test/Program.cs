using Microsoft.Extensions.DependencyInjection;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Serialisation;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse.Resolvers;

var services = new ServiceCollection();

// create mock services
var service = Mock<IOrganizationService>();
var context = Mock<IExecutionContext>();
var tracer = Mock<ITracingService>();

// core Dataverse service registrations
services.AddSingleton<IOrganizationService>(service);
services.AddSingleton<IExecutionContext>(context);
services.AddSingleton<ITracingService>(tracer);

// IDataverseContext registration
services.AddSingleton<IDataverseContext, DataverseContext>();

services.AddSingleton<IValidationEngine, ValidationEngine>();
services.AddSingleton<IValidationRuleSetProvider<StudentDto>, StrictStudentRuleSet>();

// LookupEngine registration
services.AddSingleton<ILookupEngine, LookupEngine>();

// ILookupResolver service registrations
services.AddTransient<
    ILookupResolver<int>,
    SynStudentByStudentIdResolver
>();

// add query providers
services.AddTransient<SynStudents>();

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