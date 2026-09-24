using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using spgsWebTools.XrmHelpers;
using spgsWebTools.XrmHelpers.Constants;
using spgsWebTools.XrmHelpers.Domain.Contracts;
using spgsWebTools.XrmHelpers.Domain.Services;
using spgsWebTools.XrmHelpers.Domain.Dtos;
using spgsWebTools.XrmHelpers.Serialisation;
using spgsWebTools.XrmHelpers.Infrastructure.Dataverse;
using Xunit;
using Moq;
using System.Text.Json;

var services = new ServiceCollection();

var dictionary = new Dictionary<Guid, Entity>();

var entity = new Entity(EntityLogicalNames.SynStudent)
{
    Id = Guid.NewGuid()
};

entity[SynStudentFields.StudentId] = 283224;
entity[SynStudentFields.StudentGiven1] = "Matt";
entity[SynStudentFields.StudentSurname] = "Thomas";
entity[SynStudentFields.Age] = 26;
entity[SynStudentFields.StudentYearLevel] = 12;
entity[SynStudentFields.StudentCampus] = "S";
entity[SynStudentFields.StudentGender] = "M";
entity[SynStudentFields.StudentHouse] = "CA";

dictionary[entity.Id] = entity;

var previousEntity = new Entity(EntityLogicalNames.SynStudent)
{
    Id = entity.Id
};

previousEntity[SynStudentFields.StudentId] = 283224;
previousEntity[SynStudentFields.StudentGiven1] = "Matthew"; // old value
previousEntity[SynStudentFields.StudentSurname] = "Thomas";
previousEntity[SynStudentFields.Age] = 26;

dictionary[previousEntity.Id] = previousEntity;

// create mock services
var service = new Mock<IOrganizationService>();
var context = new Mock<IExecutionContext>();
var tracer = new Mock<ITracingService>();

// configure mock services
service
    .Setup(x => x.RetrieveMultiple(It.IsAny<QueryBase>()))
    .Returns((QueryBase query) =>
    {
        return new EntityCollection(dictionary.Values.ToList());
    });

tracer
    .Setup(x => x.Trace(It.IsAny<string>(), It.IsAny<object[]>()))
    .Callback<string, object[]>((msg, args) =>
        Console.WriteLine(string.Format(msg, args)));

// core Dataverse service registrations
services.AddSingleton<IOrganizationService>(service.Object);
services.AddSingleton<IExecutionContext>(context.Object);
services.AddSingleton<ITracingService>(tracer.Object);

// IDataverseContext registration
services.AddSingleton<IDataverseContext, DataverseContext>();

// ValidationEngine registration
services.AddSingleton<IValidationEngine, ValidationEngine>();
services.AddSingleton<IValidationRuleSetProvider<StudentDto>, StrictStudentRuleSet>();

// IDataverseMapper registration
services.AddSingleton<IDataverseMapper<StudentDto>, DataverseMapper<StudentDto>>();

// ILookupEnricher registration
services.AddSingleton<ILookupEnricher<StudentDto>, LookupEnricher<StudentDto>>();

// LookupMetadataFactory registration
services.AddSingleton<ILookupMetadataFactory, LookupMetadataFactory>();

// LookupEngine registration
services.AddSingleton<ILookupEngine, LookupEngine>();

// ChangeDetectionService registration
services.AddSingleton<IChangeDetectionService<StudentDto>, ChangeDetectionService<StudentDto>>();

// HashService registration
services.AddSingleton<IHashService<StudentDto>, HashService<StudentDto>>();

// add Resolvers
services.AddTransient(
    typeof(ILookupResolver<>),
    typeof(LookupResolver<>));

var serviceProvider = services.AddXrmSerializationEngine().BuildServiceProvider();

Console.WriteLine("Start Dry Run...");

// call mapper
var dataverseMapper = serviceProvider.GetRequiredService<IDataverseMapper<StudentDto>>();
var validationEngine = serviceProvider.GetRequiredService<IValidationEngine>();
var lookupEngine = serviceProvider.GetRequiredService<ILookupEngine>();
var enricher = serviceProvider.GetRequiredService<ILookupEnricher<StudentDto>>();
var changeDetectionService = serviceProvider.GetRequiredService<IChangeDetectionService<StudentDto>>();

var oldDto = dataverseMapper.MapToDto(previousEntity);
var newDto = dataverseMapper.MapToDto(entity);

oldDto = enricher.Execute(oldDto);
newDto = enricher.Execute(newDto);

var comparison = changeDetectionService.Compare(oldDto, newDto);

Entity targetEntity = comparison.HasChanged
    ? dataverseMapper.MapToTarget(newDto)
    : dataverseMapper.MapToTarget(oldDto);