/*
using Microsoft.Xrm.Sdk;
using Moq;
using Xunit;
using Custos.XrmHelpers.Domain.Dtos;
using Custos.XrmHelpers.Constants;

public class DataverseMockTests
{
    public void InitialiseMockOrganisationService(
        Mock<IOrganizationService> mockService,
        Entity testEntity,
        Dictionary<Guid, Entity> dictionary
    )
    {
        // add Create
        mockService
            .Setup(s => s.Create(It.IsAny<Entity>()))
            .Returns((Entity entity) =>
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                dictionary[entity.Id] = entity;

                return entity.Id;
            });

        mockService
            .Setup(s => s.RetrieveMultiple(It.IsAny<QueryBase>()))
            .Returns((QueryBase query) =>
            {
                var qe = (QueryExpression)query;

                var results = dictionary.Values
                    .Where(e => e.LogicalName == qe.EntityName)
                    .ToList();

                return new EntityCollection(results);
            });
    }
    /*[Fact]
    public void TestStudentLookupById()
    {

    }*/