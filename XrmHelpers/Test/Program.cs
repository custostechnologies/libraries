using Microsoft.Extensions.DependencyInjection;
using custos.XrmHelpers;
using custos.XrmHelpers.Constants;
using custos.XrmHelpers.Domain.Contracts;
using custos.XrmHelpers.Domain.Dtos;
using custos.XrmHelpers.Serialisation;
using custos.XrmHelpers.Infrastructure.Dataverse.Resolvers;
using System.Security.Cryptography;

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
    Given1 = "Matt",
    YearLevel = 12,
    Age = 26
};

var cols = new ColumnSet(
    SynStudentFields.StudentId,
    SynStudentFields.SynMirrorId,
    SynStudentFields.StudentGiven1
);

var metadata = new LookupMetadata<int>(
    283224,
    false,
    true,
    EntityLogicalNames.SynStudent,
    SynStudentFields.StudentId,
    EntityLogicalNames.SynStudent,
    SynStudentFields.StudentId,
    cols,
    500
);

var validationEngine = serviceProvider.GetRequiredService<IValidationEngine>();
var lookupEngine = serviceProvider.GetRequiredService<ILookupEngine>();

ValidationResult report = validationEngine.Validate(student);
ILookupResult<int> luResult = lookupEngine.Execute(metadata);

Console.WriteLine("after lu resolution");

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